using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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
    PlantCareInfo1,
    PlantCareInfo2,
    WaitGrowth,
    PickPlant,
    DestroyObstacle,
    ShootTargetInfo,
    WaitNight,
    WaveWarning,
    AutoAttackInfo,
    WaitFinishWave,
    ExploreAndCollect,
    Done
}

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    public event EventHandler IntroTutorialEnd;

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
                tutorialText.text = "Use WASD/Arrow Keys to walk";
                break;

            case TutorialStep.Interact:
                tutorialText.text = "Press E to interact/pick up a object";
                break;

            case TutorialStep.DropItem:
                tutorialText.text = "Press E to drop item";
                break;

            case TutorialStep.PickWateringCan:
                tutorialText.text = "Pick up the watering can";
                break;

            case TutorialStep.FillWateringCan:
                tutorialText.text = "Watering can fills when close to the well";
                break;

            case TutorialStep.UseWateringCan:
                tutorialText.text = "While holding the watering can, click it";
                break;

            case TutorialStep.WaterPlant:
                tutorialText.text = "Water plant by clicking in it";
                break;

            case TutorialStep.PlantCareInfo1:
                tutorialText.text = "Your plants will warn you when they want water";
                CompleteInfoStep();
                break;

            case TutorialStep.PlantCareInfo2:
                tutorialText.text = "Water helps plants grow and heal.\nGrow your plant!";
                CompleteInfoStep();
                break;

            case TutorialStep.WaitGrowth:
                tutorialText.text = "";
                break;

            case TutorialStep.PickPlant:
                tutorialText.text = "You can also carry plants";
                break;

            case TutorialStep.DestroyObstacle:
                tutorialText.text = "Click the plant and aim for a wooden prop";
                break;

            case TutorialStep.ShootTargetInfo:
                tutorialText.text = "You can use your plant to hit other targets";
                CompleteInfoStep();
                IntroTutorialEnd?.Invoke(this, EventArgs.Empty);
                break;

            case TutorialStep.WaitNight:
                tutorialText.text = "";
                break;

            case TutorialStep.WaveWarning:
                tutorialText.text = "At night, monsters will go towards your house.\nPrepare yourself!";
                CompleteInfoStep();
                break;

            case TutorialStep.AutoAttackInfo:
                tutorialText.text = "Your plants will attack enemies automatically";
                CompleteInfoStep();
                break;

            case TutorialStep.WaitFinishWave:
                tutorialText.text = "";
                break;

            case TutorialStep.ExploreAndCollect:
                tutorialText.text = "Explore the area and find new plant seedlings\nto help you protect your house";
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
