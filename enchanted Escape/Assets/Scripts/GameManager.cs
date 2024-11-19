using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private List<Ghost> ghosts;
    public Pacman pacman;
    [SerializeField] private Transform pellets;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text scoreText;

    [SerializeField] GameObject[] GhostsAtma;
    [SerializeField] Transform GhostHouse;
    [SerializeField] int Time = 120;
    [SerializeField] GameObject DoorOpenPE;

    public int score { get; private set; } = 0;
    public int lives { get; private set; } = 3;

    private int ghostMultiplier = 1;

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown) {
            ManagerMain.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void SpawnTheGhost()
    {
        ghosts.Add(Instantiate(GhostsAtma[Random.Range(0,2)], GhostHouse).GetComponent<Ghost>());

        if (ghosts.Count < 4)
            Invoke(nameof(SpawnTheGhost), Random.Range(5, 9));
    }

    private void NewGame()
    {
        SetLives(1);
        NewRound();
        SetTimer();
        Invoke(nameof(SpawnThePallet), Random.Range(9, 18));
        Invoke(nameof(SpawnTheGhost), Random.Range(5, 9));
    }

    void SetTimer()
    {
        scoreText.text =  "Survive For "+ Time.ToString() + " Sec, Luma Opening The Gate...";
        InvokeRepeating(nameof(ReduceTimeByOneTillWin), 1, 1);
    }

    void ReduceTimeByOneTillWin()
    {
        Time--;
        scoreText.text = "Survive For " + Time.ToString() + " Sec, Luma Opening The Gate...";

        if (Time < 0) {
            WonTheLevel();
        }
    }

    void WonTheLevel()
    {
        scoreText.text = "Luma Opened The Gate!!!";
        DoorOpenPE.SetActive(true);
        scoreText.color = Color.green;
        KillGhost();
        //Invoke(nameof(NextScene), 3);
    }

    public void NextScene()
    {
        ManagerMain.Instance.LoadScene(3);
    }

    private void NewRound()
    {
        gameOverText.enabled = false;

        foreach (Transform pellet in pellets) {
            pellet.gameObject.SetActive(false);
        }

        ResetState();
    }

    private void SpawnThePallet()
    {
        pellets.GetChild(Random.Range(0, pellets.childCount)).gameObject.SetActive(true);

        Invoke(nameof(SpawnThePallet), Random.Range(15, 18));
    }

    private void ResetState()
    {
        for (int i = 0; i < ghosts.Count; i++) {
            ghosts[i].ResetState();
        }

        pacman.ResetState();
    }

    private void GameOver()
    {
        gameOverText.enabled = true;

        for (int i = 0; i < ghosts.Count; i++) {
            ghosts[i].gameObject.SetActive(false);
        }

        CancelInvoke(nameof(SpawnTheGhost));
        CancelInvoke(nameof(ReduceTimeByOneTillWin));
        CancelInvoke(nameof(SpawnThePallet));

        scoreText.text = "You Didnt Survive TvT";
        scoreText.color = Color.red;

        pacman.gameObject.SetActive(false);
    }

    void KillGhost()
    {
        for (int i = 0; i < ghosts.Count; i++)
        {
            ghosts[i].gameObject.SetActive(false);
        }

        CancelInvoke(nameof(SpawnTheGhost));
        CancelInvoke(nameof(SpawnThePallet));
        //pacman.gameObject.SetActive(false);
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void PacmanEaten()
    {
        pacman.DeathSequence();

        SetLives(lives - 1);

        if (lives > 0) {
            Invoke(nameof(ResetState), 3f);
        } else {
            GameOver();
        }
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * ghostMultiplier;

        ghostMultiplier++;
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < ghosts.Count; i++) {
            ghosts[i].frightened.Enable(pellet.duration);
            ghosts[i].GhostBody.color = Color.cyan;
        }

        PelletEaten(pellet);
        CancelInvoke(nameof(ResetGhostMultiplier));
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    private void ResetGhostMultiplier()
    {
        ghostMultiplier = 1;
    }

}
