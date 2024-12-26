using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private Slider bossHealthbar;
    [SerializeField] private GameObject entrance;
    [SerializeField] private GameObject statisticDisplay;
    [SerializeField] private AudioSource finalBossTheme;
    [SerializeField] private Transform playerTeleportPosition;
    [SerializeField] private Transform npcPosition;
    [SerializeField] private Transform leftPoint, rightPoint;
    private CharacterStats characterStats;
    private Vector2 center, size;
    private GameObject npc = null;

    private bool isActive = false;
    private bool hasCountedDeath = false;
    private bool sendData = false;
    private int numberOfDeaths = 0;

    private const string endpoint = "http://26.123.150.88:8080/api/game/history/add";

    void Update()
    {
        TextDisplay display = statisticDisplay.GetComponent<TextDisplay>();
        if (display != null)
        {
            Debug.Log("Not null");
        }


        Vector3 playerTransform = PlayerInstance.instance.transform.position;

        if (GemCount.instance.lastBoss && dialogueBox.GetComponent<DialogueUI>().endOfDialogue)
        {
            PlayerInstance.instance.TeleportPlayer(playerTeleportPosition.position);
            npc = GameObject.Find("NPC");
            npc.transform.position = npcPosition.position;
            characterStats = npc.GetComponent<CharacterStats>();
        }
        center = (leftPoint.position + rightPoint.position) / 2;
        size = new Vector2(Mathf.Abs(rightPoint.position.x - leftPoint.position.x), Mathf.Abs(rightPoint.position.y - leftPoint.position.y));

        Bounds areaBounds = new Bounds(center, size);
        if (areaBounds.Contains(playerTransform) && !isActive)
        {   
            EnableNPC();
            isActive = true;
        }

        if (isActive)
        {
            bossHealthbar.value = characterStats.currentHealth;
        }

        if (characterStats != null)
        {
            if (characterStats.isDead && !hasCountedDeath)
            {
                numberOfDeaths++;
                hasCountedDeath = true;
            }
            if (!characterStats.isDead && hasCountedDeath)
            {
                hasCountedDeath = false;
            }
        }
        if (numberOfDeaths == 2)
        {
            finalBossTheme.Stop();
            if (!sendData)
            {
                sendData = true;
                StartCoroutine(EndGame());
            }
        }
    }

    private void EnableNPC()
    {
        characterStats.enabled = true;
        npc.GetComponent<Animator>().enabled = true;
        npc.GetComponent<FinalBossController>().enabled = true;

        bossHealthbar.gameObject.SetActive(true);
        bossHealthbar.maxValue = characterStats.maxHealth;
        entrance.SetActive(true);
        finalBossTheme.Play();
    }

    [Serializable]
    public class AddHistoryReq
    {
        public int userId;
        public int death;
        public int time;
        public int level;
    }

    private IEnumerator EndGame()
    {

        var historyReq = new AddHistoryReq
        {
            userId = PlayerPrefs.GetInt("UserId"),
            death = 1,
            time = 100,
            level = 1
        };

        string jsonData = JsonUtility.ToJson(historyReq);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest(endpoint, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, size);
    }
}