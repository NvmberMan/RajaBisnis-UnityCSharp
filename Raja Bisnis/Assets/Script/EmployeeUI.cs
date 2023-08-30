using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmployeeUI : MonoBehaviour
{
    public Employee thisEmployee;
    public Image display;
    public TMP_Text levelText;

    public void viewEmployee()
    {
        EmployeeManager.instance.viewEmployee(thisEmployee);
    }
}
