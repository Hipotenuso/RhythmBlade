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
    [Header("Int Values")]
    public int _index;
    public int value;
    public int actualValue;
    public int activeEvents;
    public int attack = 0;
    public int misses = 0;
    [Header("Float Values")]
    public float actualRando;
    public float Rando1;
    public float Rando2;
    public float Rando3;
    public float gap1;
    public float gap2;
    public float gap3;
    public float actualGap;
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
    public TextMeshProUGUI textRecord;
    void Start()
    {
        if(rhythm == null) rhythm = FindAnyObjectByType<Rhythm>();
        if(box == null) box = FindAnyObjectByType<Box>();
        activeEvents = 0;
        attack = 0;
        Interval();
        ResetInfos();
        LoadScore();
        RandosAtt();
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
            if(actualBox1 == null || attack == 0)
            {
                Invoke(nameof(Event), actualGap);
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
    private void RandosAtt()
    {
        Rando1 = Random.Range(0.5f, 0.7f);
        Rando2 = Random.Range(0.25f, 0.45f);
        Rando3 = Random.Range(0.12f, 0.22f);
        gap1 = Random.Range(0.8f, 1.2f);
        gap2 = Random.Range(0.2f, 0.6f);
        gap3 = Random.Range(0.02f, 0.3f);
    }
    IEnumerator Lights()
    {
        actualBox1.sprite = miss;
        value = 0;
        if (attack != 1)
            yield return new WaitForSeconds(actualRando);

        actualBox1.sprite = good;
        value = 2;
        if (attack != 1)
            yield return new WaitForSeconds(actualRando);

        actualBox1.sprite = great;
        value = 8;
        if (attack != 1)
            yield return new WaitForSeconds(actualRando);

        actualBox1.sprite = perfect;
        value = 32;
        if (attack != 1)
            yield return new WaitForSeconds(actualRando);

        erros.value += 1;
        rhythm.charge -= 5;
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
            rhythm.charge += 2;
            bons.value++;
        }
        if(actualBox1.sprite == great)
        {
            rhythm.charge += 4;
            otimos.value++;
        }
        if(actualBox1.sprite == perfect)
        {
            rhythm.charge += 8;
            perfeitos.value++;
        }
        pontos.value += value*box.stageBonus;
        attack = 1;
        EndEvent();
        ResetValue();
        UpdateUI();
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
            textRecord.text = record.value.ToString();
            SaveScore();
        }
        else
        {
            return;
        }
    }
    public void LoadScore()
    {
        record.value = PlayerPrefs.GetInt("HighScore", record.value);
        textRecord.text = record.value.ToString();
    }
    public void SaveScore()
    {
        PlayerPrefs.SetInt("HighScore", record.value);
    }
}