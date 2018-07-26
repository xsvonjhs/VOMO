using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccessData : MonoBehaviour {

    public InputField input;
    public string extract_firstname;
    public string extract_lastname;
    public string extract_username;
    public string extract_password;
    public string extract_mobile;
    public string extract_email;

    public string extract_Date;
    public string extract_Month;
    public string extract_Year;

    public string extract_Gender = "0";

    public Transform Birthday_date;
    public Transform Birthday_month;
    public Transform Birthday_year;
    private int Bday_date;
    private int Bday_month;
    private int Bday_year;


    public bool allowRegister = false;
    public GameObject Reg_panel;
    public Text Register_warntext = null;
    public GameObject Back_to_Reg_button;
    public GameObject Login_button;
    public static string Server = "vomoapps.000webhostapp.com";
    public string CreateUserURL = Server+"/InsertUser_internet.php"; 

    public void Awaking()
    {
       

    }
    // Use this for initialization
    void Start () {

	}

    public void Gender_Male()
    {
        extract_Gender = "Male";
    }

    public void Gender_Female()
    {
        extract_Gender = "Female";
    }


    public void CreateUser()
    {
        input = GameObject.Find("In_Firstname").GetComponent<InputField>();
        extract_firstname = input.text;

        input = GameObject.Find("In_Lastname").GetComponent<InputField>();
        extract_lastname = input.text;

        input = GameObject.Find("In_Username").GetComponent<InputField>();
        extract_username = input.text;

        input = GameObject.Find("In_Password").GetComponent<InputField>();
        extract_password = input.text;

        input = GameObject.Find("In_Mobile").GetComponent<InputField>();
        extract_mobile = input.text;

        input = GameObject.Find("In_Email").GetComponent<InputField>();
        extract_email = input.text;

        Bday_date = Birthday_date.GetComponent<Dropdown>().value;
        Bday_month = Birthday_month.GetComponent<Dropdown>().value;
        Bday_year = Birthday_year.GetComponent<Dropdown>().value;

        List<Dropdown.OptionData> DateOptions = Birthday_date.GetComponent<Dropdown>().options;
        List<Dropdown.OptionData> MonthOptions = Birthday_month.GetComponent<Dropdown>().options;
        List<Dropdown.OptionData> YearOptions = Birthday_year.GetComponent<Dropdown>().options;

        extract_Date = DateOptions[Bday_date].text;
        extract_Month = MonthOptions[Bday_month].text;
        extract_Year = YearOptions[Bday_year].text;



        StartCoroutine(FeedbackFromSite(extract_firstname, extract_lastname, extract_username, extract_password, extract_email, extract_mobile, extract_Date, extract_Month, extract_Year, extract_Gender));

    }

    IEnumerator FeedbackFromSite(string user_firstname, string user_lastname, string user_username, string user_password, string user_email, string user_mobile, string user_Bdate, string user_Bmonth, string user_Byear, string user_gender)
    {


        Debug.Log("You entered: " + extract_firstname);
        Debug.Log("You entered: " + extract_lastname);
        Debug.Log("You entered: " + extract_username);
        Debug.Log("You entered: " + extract_password);
        Debug.Log("You entered: " + extract_mobile);
        Debug.Log("You entered: " + extract_email);
        Debug.Log("You entered: " + extract_Date);
        Debug.Log("You entered: " + extract_Month);
        Debug.Log("You entered: " + extract_Year);
        Debug.Log("You entered: " + extract_Gender);


        WWWForm form = new WWWForm();
        form.AddField("namapertama", extract_firstname);
        form.AddField("namaakhir", extract_lastname);
        form.AddField("penggunaid", extract_username);
        form.AddField("katalaluan", extract_password);
        form.AddField("email", extract_email);
        form.AddField("mobilenum", extract_mobile);
        form.AddField("date", extract_Date);
        form.AddField("month", extract_Month);
        form.AddField("year", extract_Year);
        form.AddField("gender", extract_Gender);



        WWW www = new WWW("https://" + CreateUserURL, form);

        Debug.Log("now wait!");
        yield return www;
        Debug.Log(www.text);


        if (www.text == "Duplicate")
        {
            Reg_panel.SetActive(true);
            Register_warntext.text = "Registration Failed. This username or email might be used.";
            Back_to_Reg_button.SetActive(true);
            Login_button.SetActive(false);
        }

        else if (www.text == "Everything OK")
        {
            Reg_panel.SetActive(true);
            Register_warntext.text = "Registration succesful. Please check your email for account activation.";
            Login_button.SetActive(true);
            Back_to_Reg_button.SetActive(false);
        }

        else if(www.text == "Incomplete Data")
        {
            Reg_panel.SetActive(true);
            Register_warntext.text = "Incomplete data.";
            Login_button.SetActive(false);
            Back_to_Reg_button.SetActive(true);
        }



    }

}
