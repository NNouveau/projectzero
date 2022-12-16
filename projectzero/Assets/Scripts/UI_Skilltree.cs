using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Skilltree : MonoBehaviour
{
    public static UI_Skilltree skillTree;
    private void Awake()=>skillTree=this;
    public int[] skillLevels;
    public int[] skillCaps;
    public int[] skillCost;
    public string[] skillNames;
    public string[] skillDesc;

    public List<Skill> skillList;
    public GameObject skillHolder;
    public List<GameObject> connectorList;
    public GameObject connectorHolder;
    public int skillPoint;

    private void Start()
    {
        skillPoint = 20;

        skillLevels = new int[12];
        skillCaps = new[] {1,1,1,1,1,1,1,1,1,1,1,1};
        skillCost = new[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        skillNames = new[] { "Katana", "Katana1", "Katana2", "Bow", "Bow2", "Bow3", "Dash", "Dash1", "Dash2", "Stats", "Stats1", "Stats2" };
        skillDesc = new[] { "Kombo açýlýr", "Kombo açýlýr2", "Kombo açýlýr3", "Chargeshot Açýlýr.", "Chargeshot Açýlýr.2", "Chargeshot Açýlýr.3", "Dash atarkan hasar almazsýn.", "Dash atarkan hasar almazsýn.2", "Dash atarkan hasar almazsýn.3", "Can ve Saldýrý gücün artar", "Can ve Saldýrý gücün artar2", "Can ve Saldýrý gücün artar3" };

        foreach (var skill in skillHolder.GetComponentsInChildren<Skill>())
            skillList.Add(skill);
        foreach (var connector in connectorHolder.GetComponentsInChildren<RectTransform>())
            connectorList.Add(connector.gameObject);
        for (var i = 0; i < skillList.Count; i++)
            skillList[i].id = i;

        skillList[0].ConnectedSkills = new[] { 1 };
        skillList[1].ConnectedSkills = new[] { 2 };
        skillList[3].ConnectedSkills = new[] { 4 };
        skillList[4].ConnectedSkills = new[] { 5 };
        skillList[6].ConnectedSkills = new[] { 7 };
        skillList[7].ConnectedSkills = new[] { 8 };
        skillList[9].ConnectedSkills = new[] { 10 };
        skillList[10].ConnectedSkills = new[] { 11 };

        UpdateSkillUI();
    }

    public void UpdateSkillUI()
    {
        foreach (var skill in skillList) skill.UpdateUI();
    }

}
