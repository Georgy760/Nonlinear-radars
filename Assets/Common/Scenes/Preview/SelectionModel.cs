using System.Collections;
using System.Collections.Generic;
using Common.Scenes.Preview;
using Common.Scenes.Preview.Scripts;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using DeviceType = Common.Scenes.Preview.Scripts.DeviceType;

public class SelectionModel : MonoBehaviour
{
    public Vector3 firstPos;
    public float spacingBetweenModel;

    public float selectedModelSize;
    public float unselectedModelSize;

    public float modelSwitchDuration;

    public List<GameObject> models;
    public List<GameObject> duplicatedmodels;

    public GameObject previousModel;
    public GameObject currentModel;
    public GameObject nextModel;
    public GameObject hiddenModel;
    [SerializeField] private bool playOnStart;
    [SerializeField] private int index;
    private bool modelOnlyOne = false;
    
    public Sequence moveSequence;
    private IControllerService _controllerService;

    [Inject]
    private void Construct(IControllerService controllerService)
    {
        _controllerService = controllerService;
        _controllerService.OnPrevModel += PreviousModel;
        _controllerService.OnNextModel += NextModel;
        _controllerService.OnSelectModel += Select;
    }
    private void OnDestroy()
    {
        _controllerService.OnPrevModel -= PreviousModel;
        _controllerService.OnNextModel -= NextModel;
        _controllerService.OnSelectModel -= Select;
    }
    public void Awake()
    {
        if (playOnStart) Initialize(); 
    }

    private void Select()
    {
        switch (currentModel.GetComponent<Device>().DeviceType)
        {
            case DeviceType.Orion:
                SceneManager.LoadScene("Orion24");
                break;
            case DeviceType.EMS:
                SceneManager.LoadScene("EMS");
                break;
            case DeviceType.Scorpion:
                
                break;
        }
    }

    private void TryPreviousModel()
    {
        if (!modelOnlyOne)
        {
            PreviousModel();
        }
    }

    private void TryNextModel()
    {
        if (!modelOnlyOne)
        {
            NextModel();
        }
    }

    private void Touch(Vector2 obj)
    { 
        if (!modelOnlyOne)
        {
            if (obj.x > 0.71f && obj.y > -0.71f && obj.y < 0.71f) NextModel();
        if (obj.x < -0.71f && obj.y > -0.71f && obj.y < 0.71f) PreviousModel(); 
        }
}

    
    [ContextMenu("Initialize")]
    public void Initialize()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        for(int i = 0; i < models.Count; i++)
        {
            var model = Instantiate(models[i], transform, false) as GameObject;
            model.transform.position = new Vector3(0f, -50f, 0f);
            model.GetComponent<Turnable>().enabled = true;
            models[i] = model;

        }
        
        index = 0;
        currentModel = models[index];
        models[index].transform.position = firstPos;
        models[index].transform.localScale = Vector3.one * selectedModelSize;
        previousModel = null;
        nextModel = null;
        
            duplicatedmodels = new List<GameObject>();
            var duplicatedModel = Instantiate(models[0], new Vector3(0f, -50f, 0f),
                Quaternion.Euler(new Vector3(349.5f,315f,10.5f)));
            duplicatedModel.transform.SetParent(gameObject.transform, false);
            
            duplicatedmodels.Add(duplicatedModel);
            duplicatedModel = Instantiate(models[1], new Vector3(0f, -50f, 0f),
                Quaternion.Euler(new Vector3(349.5f,315f,10.5f)));
            
            duplicatedModel.transform.SetParent(gameObject.transform, false);
            duplicatedmodels.Add(duplicatedModel);
            nextModel = models[1];
                previousModel = duplicatedmodels[1];
        

        if (!modelOnlyOne)
        {
            nextModel.transform.position = firstPos + new Vector3(spacingBetweenModel, 0, 0);
            nextModel.transform.localScale = Vector3.one * unselectedModelSize;
            previousModel.transform.position = firstPos - new Vector3(spacingBetweenModel, 0, 0);
            previousModel.transform.localScale = Vector3.one * unselectedModelSize;
        }
    }

    public void UpdateModelList(List<GameObject> model)
    {
        models = new List<GameObject>();
        foreach (var obj in model)
        {
            obj.transform.rotation = Quaternion.Euler(new Vector3(349.5f,315f,10.5f));
            models.Add(obj);
        }
        Debug.Log("Models(PlayerSelectionMenu.Count: ): " + models.Count);
    }

    public void AddModelToList(GameObject model)
    {
        models.Add(model);
        Debug.Log("Added " + model.name + " model to list.\n" + "List current count: " + models.Count);
    }

    [ContextMenu("Next Model")]
    public void NextModel()
    {
        if (moveSequence != null) return;

        // Debug.Log("Next");

        index++;
        if (index >= models.Count) index = 0;

        StartMoveModels(true);
    }


    [ContextMenu("Previous Model")]
    public void PreviousModel()
    {
        if (moveSequence != null) return;

        //Debug.Log("Previous");

        index--;
        if (index < 0) index = models.Count - 1;

        StartMoveModels(false);
    }

    public void StartMoveModels(bool right)
    {
        if (moveSequence != null) return;

        var mySequence = DOTween.Sequence();
        moveSequence = mySequence;
        mySequence.Insert(0, currentModel.transform.DOScale(unselectedModelSize, modelSwitchDuration));
        if (right) //                                                                                                                                   ----------------------->
        {
            mySequence.Insert(0, previousModel.transform.DOScale(unselectedModelSize, modelSwitchDuration));
            mySequence.Insert(0, nextModel.transform.DOScale(selectedModelSize, modelSwitchDuration));
        }
        else //                                                                                                                                   <-----------------------
        {
            mySequence.Insert(0, previousModel.transform.DOScale(selectedModelSize, modelSwitchDuration));
            mySequence.Insert(0, nextModel.transform.DOScale(unselectedModelSize, modelSwitchDuration));
        }

        GameObject hiddenModel = null;
        
            if (index == 1)
            {
                hiddenModel = duplicatedmodels[0];
                if (hiddenModel == previousModel || hiddenModel == nextModel || hiddenModel == currentModel)
                {
                    //  Debug.Log("Switch1");
                    hiddenModel = models[0];
                    Debug.Log(hiddenModel.name);
                }
                    
            }
            else
            {
                hiddenModel = duplicatedmodels[1];
                if (hiddenModel == previousModel || hiddenModel == nextModel || hiddenModel == currentModel)
                {
                    //  Debug.Log("Switch2");
                    hiddenModel = models[1];
                    Debug.Log(hiddenModel.name);
                }
                    
            }
        

        hiddenModel.transform.localScale = Vector3.one * unselectedModelSize;

        if (right) //                                                                                                                                   ----------------------->
        {
            hiddenModel.transform.position = firstPos + new Vector3(spacingBetweenModel, 0, 0) * 2;
            //hiddenModel.name = "HiddenOne";
            mySequence.Insert(0, hiddenModel.transform.DOMove(nextModel.transform.position, modelSwitchDuration));

            
            mySequence.Insert(0, currentModel.transform.DOMove(previousModel.transform.position, modelSwitchDuration));
            mySequence.Insert(0, nextModel.transform.DOMove(currentModel.transform.position, modelSwitchDuration));
            previousModel.transform.position -= new Vector3(spacingBetweenModel + 50, 0, 0);
            //previousModel.transform.
            previousModel = currentModel;
            currentModel = nextModel;
            nextModel = hiddenModel;
        }
        else //                                                                                                                                   <-----------------------
        {
            hiddenModel.transform.position = firstPos - new Vector3(spacingBetweenModel, 0, 0) * 2;
            
            mySequence.Insert(0, hiddenModel.transform.DOMove(previousModel.transform.position, modelSwitchDuration));

            mySequence.Insert(0, currentModel.transform.DOMove(nextModel.transform.position, modelSwitchDuration));
            mySequence.Insert(0, previousModel.transform.DOMove(currentModel.transform.position, modelSwitchDuration));
            nextModel.transform.position += new Vector3(spacingBetweenModel + 50, 0, 0);

            nextModel = currentModel;
            currentModel = previousModel;
            previousModel = hiddenModel;
        }

        mySequence.OnComplete(ResetSequence);
    }

    public void SetNewModelsTarget(bool right)
    {
    }

    public void ResetSequence()
    {
        moveSequence = null;
    }
}
