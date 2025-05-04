using System.Collections;
using TMPro;
using UnityEngine;

public class Infinite : MonoBehaviour
{
    public Box box;
    public Rhythm rhythm;
    public Coroutine lights;
    [Header("Boxes")]
    public SpriteRenderer frontBox;
    public SpriteRenderer backBox;
    public SpriteRenderer leftBox;
    public SpriteRenderer rightBox;
    [Header("Renders")]
    public SpriteRenderer actualBox1;
    public Sprite miss;
    public Sprite good;
    public Sprite great;
    public Sprite perfect;
    [Header("Values")]
    public int _index;
    public int value;
    public int actualValue;
    public int activeEvents;
    public int attack = 0;
    public int misses = 0;
    [Header("SO")]
    public SOInt pontos;
    public SOInt perfeitos;
    public SOInt otimos;
    public SOInt bons;
    public SOInt erros;
    public SOInt record;
    [Header("Texts")]
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textPerfects;
    public TextMeshProUGUI textGreat;
    public TextMeshProUGUI textGood;
    public TextMeshProUGUI textMiss;
    void Start()
    {
        if(rhythm == null) rhythm = FindAnyObjectByType<Rhythm>();
        if(box == null) box = FindAnyObjectByType<Box>();
        activeEvents = 0;
        attack = 0;
        Interval();
        ResetInfos();
        LoadScore();
    }
  private void RandomBox()
    {
        SpriteRenderer[] boxes = {frontBox, backBox, leftBox, rightBox};
        _index = Random.Range(0, boxes.Length);
        actualBox1 = boxes[_index];
    }
    private void Interval()
    {
        if(activeEvents == 0)
        {
            float gap = Random.Range(0.1f, 0.4f);
            if(actualBox1 == null || attack == 0)
            {
                Invoke(nameof(Event), gap);
            }
        }
        else
        {
            return;
        }
    }
    private void Event()
    {
        activeEvents = 1;
        RandomBox();
        lights = StartCoroutine(Lights());
    }
    IEnumerator Lights()
    {
        float rando1 = Random.Range(0.5f, 0.7f);
        float rando2 = Random.Range(0.2f, 0.4f);
        float rando3 = Random.Range(0.2f, 1f);
        actualBox1.sprite = miss;
        value = 0;
        if (attack != 1)
            yield return new WaitForSeconds(rando1);

        actualBox1.sprite = good;
        value = 2;
        if (attack != 1)
            yield return new WaitForSeconds(rando2);

        actualBox1.sprite = great;
        value = 8;
        if (attack != 1)
            yield return new WaitForSeconds(rando3);

        actualBox1.sprite = perfect;
        value = 32;
        if (attack != 1)
            yield return new WaitForSeconds(0.3f);

        erros.value += 1;
        rhythm.charge -= 10;
        UpdateUI();
        EndEvent();
}
    public void EndEvent()
    {
        ResetSprites();
        if(lights != null) StopCoroutine(lights);
        activeEvents = 0;
        attack = 0;
        Interval();
    }
    public void GetScore()
    {
        if (attack == 1 || activeEvents == 0 || lights == null || _index != rhythm.hit)
        {
            return;
        }
        if(actualBox1.sprite == miss)
        {
            rhythm.charge -= 10;
            erros.value++;
        }
        if(actualBox1.sprite == good)
        {
            bons.value++;
        }
        if(actualBox1.sprite == great)
        {
            otimos.value++;
        }
        if(actualBox1.sprite == perfect)
        {
            perfeitos.value++;
        }
        attack = 1;
        EndEvent();
        pontos.value += value*box.stageBonus;
        ResetValue();
        UpdateUI();
    }
    private void Miss()
    {
        if(misses != 0)
        {
            rhythm.charge -= 10;
            UpdateUI();
            misses -= 1;
        }
    }
    public void ResetInfos()
    {
        pontos.value = 0;
        perfeitos.value = 0;
        otimos.value = 0;
        bons.value = 0;
        erros.value = 0;
    }
    public void ResetValue()
    {
        value = 0;
    }
    private void ResetSprites()
    {
        if(actualBox1 != null)
        {
            actualBox1.sprite = miss;
        }
    }
    private void UpdateUI()
    {
        textScore.text = pontos.value.ToString();
        textPerfects.text = perfeitos.value.ToString();
        textGreat.text = otimos.value.ToString();
        textGood.text = bons.value.ToString();
        textMiss.text = erros.value.ToString();
        CheckRecord();
    }
    private void CheckRecord()
    {
        if(pontos.value >= record.value)
        {
            record.value = pontos.value;
        }
        else
        {
            return;
        }
    }
    public void LoadScore()
    {
        record.value = PlayerPrefs.GetInt("HighScore", record.value);
    }
}