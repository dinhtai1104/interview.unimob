using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _ins;
    public static GameManager Instance
    {
        get
        {
            if (!_ins)
            {
                _ins = FindObjectOfType<GameManager>();
            }
            return _ins;
        }
    }
    public Transform targetPos;

    private List<SpiderMovement> spiderMovementsPool;
    public SpiderMovement spiderPrefab;
    public List<Transform> checkPoints;

    [SerializeField]
    private int currentUnits;
    public Text unitsCountText;


    private void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    public void Reach()
    {
        currentUnits--;
        if (currentUnits <= 0) currentUnits = 0;
        unitsCountText.text = "Units: " + currentUnits;
    }

    public SpiderMovement RequestSpider()
    {
        if (spiderMovementsPool == null) spiderMovementsPool = new List<SpiderMovement>();
        SpiderMovement spi = spiderMovementsPool.Find(t => t.gameObject.activeSelf == false);
        if (!spi)
        {
            spi = Instantiate(spiderPrefab, checkPoints[Random.Range(0, checkPoints.Count)].position, Quaternion.identity, transform);
            spiderMovementsPool.Add(spi);
        }
        spi.transform.position = checkPoints[Random.Range(0, checkPoints.Count)].position;
        spi.gameObject.SetActive(true);
        return spi;
    }

    public void RequestUnitsOnClicked(int amount)
    {
        currentUnits += amount;
        unitsCountText.text = "Units: " + currentUnits;
        StartCoroutine(RequestUnitsIE(amount));
    }

    private IEnumerator RequestUnitsIE(int amount)
    {
        WaitForSeconds wfs = new WaitForSeconds(3.0f / amount);
        for (int i = 0; i < amount; i++)
        {
            RequestSpider();
            yield return wfs;
        }
    }
}
