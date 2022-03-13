using System.Linq;
using TMPro;
using UnityEngine;

[System.Serializable]
public class AttributesHudHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text[] attributesValueTexts;

    public void ChangeAttributeValueText<T>(string containsName, T value)
    {
        Debug.Log($"Changing {containsName} stat to value: {value}");

        TMP_Text textObject = attributesValueTexts.FirstOrDefault(t => t.name.Contains(containsName));
        if (textObject is null)
        {
            Debug.LogError($"There is no text gameobject in attributes that contains {containsName}, value will be not changed!");
        }

        textObject.text = $"{value}";
    }
}
