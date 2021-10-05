using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ElRaccoone.Tweens;
using TMPro;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public CanvasGroup menu;
    public CanvasGroup fade;

    public GameObject unicycle;

    public int score;
    public int highscore;

    public TextMeshProUGUI scoreDisplay;
    public TextMeshProUGUI hscoreDisplay;
    public TextMeshProUGUI endScreen;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        Instantiate(unicycle).SetActive(true);
        fade.TweenCanvasGroupAlpha(0, 0.5f);
    }

    public void EndGame() {
        
    }

    public void Score(int amt) {
        score += amt;
        scoreDisplay.transform.TweenLocalScaleY(1.1f, 0.05f).SetOnComplete(() => {
            scoreDisplay.text = $"{score}<br><size=20>HS: {highscore}</size>";
            scoreDisplay.transform.TweenLocalScaleY(1f, 0.05f);
        });
        FindObjectOfType<RouteGenerator>().Generate();
        
    }

    public void LoadGame() {
        StartCoroutine(LoadGameE());

    }

    private IEnumerator LoadGameE() {
        FindObjectOfType<Unicycle>().tiltU += 15;
        FindObjectOfType<Unicycle>().tiltV += Random.Range(-10f,10f);
        var w = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        yield return null;
        while (w.progress < 0.8) {
            yield return null;
        }
        w.allowSceneActivation = true;
        yield return null;
        
        menu.TweenCanvasGroupAlpha(0, 0.5f).SetOnComplete(() => menu.gameObject.SetActive(false));
        score = 0;
    }

    public void Die() {
        Destroy(GameObject.Find("Unicycle(Clone)"), .5f);
        endScreen.text = $@"<size=42><b>oof</b></size>

Your score: {score}
<size={(score>highscore?34:24)}>{(score>highscore?"New":"Your")} highscore: {(score>highscore?score:highscore)}</size>
";
        fade.TweenCanvasGroupAlpha(1, 0.5f).SetOnComplete(() => {
            menu.gameObject.SetActive(true);
            menu.alpha = 1;
            SceneManager.UnloadSceneAsync(1);

            fade.TweenCanvasGroupAlpha(0, 0.5f).SetDelay(1);
            //destroy player
            
            Instantiate(unicycle).SetActive(true);
        });
        if (score > highscore) {
            highscore = score;
        }

        score = 0;
        Score(0);
    }

    public void Quit() {
        Application.Quit();
    }
}