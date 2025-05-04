using UnityEngine;

public class SaveScore : MonoBehaviour
{
    public Infinite infinite;
    public void SaveScoree()
    {
        PlayerPrefs.SetInt("HighScore", infinite.record.value);
    }
}
