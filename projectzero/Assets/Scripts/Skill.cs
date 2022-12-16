using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UI_Skilltree;

public class Skill : MonoBehaviour
{
    public int id;

    public TMP_Text TitleText;
    public TMP_Text DescriptionText;

    public int[] ConnectedSkills;

    public void UpdateUI()
    {
        TitleText.text = $"{skillTree.skillLevels[id]}/{skillTree.skillCaps[id]}\n{skillTree.skillNames[id]}";
        DescriptionText.text= $"{skillTree.skillDesc[id]}\n cost:{skillTree.skillCost[id]}/{skillTree.skillPoint}";

        GetComponent<Image>().color = skillTree.skillLevels[id] >= skillTree.skillCaps[id] ? Color.yellow : 
            skillTree.skillPoint >= skillTree.skillCost[id] ? Color.green : Color.white;

        foreach (var connectedSkill in ConnectedSkills)
        {
            skillTree.skillList[connectedSkill].gameObject.SetActive(skillTree.skillLevels[id] > 0);
            skillTree.connectorList[connectedSkill].SetActive(skillTree.skillLevels[id] > 0);
        }
    }

    public void UpgradeSkill()
    {
        if (skillTree.skillCost[id] > skillTree.skillPoint || skillTree.skillLevels[id] >= skillTree.skillCaps[id])
            return;
        skillTree.skillPoint -= skillTree.skillCost[id];
        skillTree.skillLevels[id]++;
        skillTree.UpdateSkillUI();
    }
}
