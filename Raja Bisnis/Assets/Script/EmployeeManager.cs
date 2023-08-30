using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmployeeManager : MonoBehaviour
{
    public static EmployeeManager instance;
    private void Awake() { instance = this; }

    private ShopObject currentShopSelected;

    [Range(0, 100)]
    public float serveCounter;

    [Header("Board")]
    public Image display;
    public TMP_Text titleText;
    public Slider serveSlider;
    public Slider restSlider;
    public Slider staminaSlider;
    public Slider attitudeSlider;

    [Space(5)]
    public GameObject employeeItemPrefab;
    public Transform employeeGrid;

    //public Employee[] ownEmployeeObjects;
    //public Employee[] employeeObjects;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateEmployee()
    {

        currentShopSelected = GameManager.instance.currentShopSelected;

        //set default view Employee
        //if (currentShopSelected.employee.Count > 0)
        //{
        //    viewEmployee(currentShopSelected.employee[0]);
        //}

        foreach (Transform child in employeeGrid)
        {
            Destroy(child.gameObject);
        }

        //Clone employee in container
        //for(int i = 0; i < currentShopSelected.employee.Count; i++)
        //{
        //    EmployeeUI em = Instantiate(employeeItemPrefab, employeeGrid).GetComponent<EmployeeUI>();

        //    em.thisEmployee = currentShopSelected.employee[i];

        //    if(currentShopSelected.employee[i].display != null)
        //        em.display.sprite = currentShopSelected.employee[i].display;

        //    em.levelText.text = currentShopSelected.employee[i].currentLvl.ToString();

        //}
    }



    public void viewEmployee(Employee employee)
    {
        if(employee.display != null)
            display.sprite = employee.display;

        titleText.text = employee.name + " Lvl." +  employee.currentLvl.ToString();
        restSlider.value = employee.restSpeed;
        serveSlider.value = employee.serveSpeed;
        staminaSlider.value = employee.stamina;
        attitudeSlider.value = employee.attitude;
    }
}
