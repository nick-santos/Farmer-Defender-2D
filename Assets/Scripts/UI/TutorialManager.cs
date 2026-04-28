using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum TutorialPhase
{
    Intro,
    Abilities,
    Combat,
    Exploration,
    Finished,
    Empty
}

public enum TutorialStep
{
    Move,
    Interact,
    DropItem,
    PickWateringCan,
    FillWateringCan,
    UseWateringCan,
    WaterPlant,
    PickPlant,
    DestroyObstacle,
    ShootTargetInfo,
    ExploreAndCollect,
    Done
}

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    [Header("UI")]
    public TMP_Text tutorialText;
    public GameObject tutorialPanel;

    [Header("Current Step")]
    public TutorialStep currentStep = TutorialStep.Move;

    [Header("Sounds")]
    public AudioSource stepCompletedAudio;

    private bool stepCompleted = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateTutorialUI();
    }

    void UpdateTutorialUI()
    {
        stepCompleted = false;

        switch (currentStep)
        {
            case TutorialStep.Move:
                tutorialText.text = "Use WASD to walk";
                break;

            case TutorialStep.Interact:
                tutorialText.text = "Get closer and press E to interact/pick up";
                break;

            case TutorialStep.DropItem:
                tutorialText.text = "Press E to drop item";
                break;

            case TutorialStep.PickWateringCan:
                tutorialText.text = "Pick up the watering can";
                break;

            case TutorialStep.FillWateringCan:
                tutorialText.text = "Take the watering can to the well to fill it";
                break;

            case TutorialStep.UseWateringCan:
                tutorialText.text = "Left click the watering can to use it";
                break;

            case TutorialStep.WaterPlant:
                tutorialText.text = "Water the damaged plant";
                break;

            case TutorialStep.PickPlant:
                tutorialText.text = "You can also carry plants";
                break;

            case TutorialStep.DestroyObstacle:
                tutorialText.text = "Use the plant to destroy the obstacle";
                break;

            case TutorialStep.ShootTargetInfo:
                tutorialText.text = "You can use the plant to hit other things";
                CompleteInfoStep();
                break;

            // case TutorialStep.AutoAttackInfo:
            //     tutorialText.text = "And plants attack enemies automatically";
            //     CompleteInfoStep();
            //     break;

            case TutorialStep.ExploreAndCollect:
                tutorialText.text = "Explore the area and find new plant seedlings";
                CompleteInfoStep();
                break;

            case TutorialStep.Done:
                FinishTutorial();
                break;
        }
    }

    public void CompleteCurrentStep(TutorialStep completedStep) // later will be coroutine
    {
        if (stepCompleted) return;

        if (completedStep != currentStep) return;

        stepCompleted = true;

        stepCompletedAudio.Play();

        Invoke("GoToNextStep", 1);
    }

    public void CompleteInfoStep()
    {
        if (stepCompleted) return;

        stepCompleted = true;

        Invoke("GoToNextStep", 5);
    }

    void GoToNextStep()
    {
        if (currentStep != TutorialStep.Done)
        {
            currentStep++;
            UpdateTutorialUI();
        }
    }

    void FinishTutorial()
    {
        currentStep = TutorialStep.Done;
        tutorialText.text = "";
    }
}
