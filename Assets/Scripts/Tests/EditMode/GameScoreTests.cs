using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class GameScoreTests
{
    private GameObject gameScoreObject;
    private GameScore gameScore;
    private Text scoreTextUI;

    [SetUp]
    public void SetUp()
    {
        gameScoreObject = new GameObject();
        scoreTextUI = gameScoreObject.AddComponent<Text>();
        gameScore = gameScoreObject.AddComponent<GameScore>();
        gameScoreObject.AddComponent<Canvas>(); // Add a Canvas to the GameObject for the Text component
        gameScoreObject.SetActive(true); // Ensure the GameObject is active

        // Manually call Start to initialize scoreTextUI
        gameScore.Start();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(gameScoreObject);
    }

    [Test]
    public void ScoreProperty_SetValue_UpdatesScoreCorrectly()
    {
        gameScore.Score = 12345;
        Assert.AreEqual(12345, gameScore.Score);
    }

    [Test]
    public void Update_SetsScoreTextUIWithFormattedScore()
    {
        gameScore.Score = 123;
        gameScore.Update();

        Assert.AreEqual("000123", scoreTextUI.text);
    }
}