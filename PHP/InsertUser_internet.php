<?php
require("connect.php");

$firstname = $_POST["namapertama"]; 
$lastname = $_POST["namaakhir"]; 
$username = $_POST["penggunaid"];
$password = $_POST["katalaluan"];
$email = $_POST["email"];
$mobile = $_POST["mobilenum"];
$date = $_POST["date"]?$_POST["date"]:"Date";
$month = $_POST["month"]?$_POST["month"]:"Month";
$year = $_POST["year"]?$_POST["year"]:"Year";
$gender = $_POST["gender"];
$token = md5($_POST["email"].time());

if (  ($firstname != "") && ($lastname != "") && ($username != "") && ($password != "") && ($email != "") && ($mobile != "") && ($date != "Date") && ($month != "Month") && ($year != "Year") && ($gender !="0") && (isset($token))  )    
{
    $CheckClash = "SELECT * FROM VOMO WHERE (username='$username' OR email='$email')";
    $Clashresult = mysqli_query($link, $CheckClash);
    $num = mysqli_num_rows($Clashresult);
    
    if ($num >=1)
    {
         echo "Duplicate";
        
    }   
    else
    {
        $sql = "INSERT INTO VOMO (username, password, first_name, last_name, mobile, email, bday_year, bday_month, bday_day, gender, token)
        VALUES ('$username' , '$password' , '$firstname' , '$lastname' , '$mobile' , '$email' , '$year' , '$month' , '$date' , '$gender' , '$token')";
        $stmt = $link->prepare($sql);
        if($stmt === false){
            http_response_code(404);
            die(mysqli_error($link));
        }

        if(!$stmt->execute()){
            die(mysqli_error($link));
        }
        $stmt->close();
        //Send Activation Email
        $actual_link = "http://vomoapps.000webhostapp.com/activateAccount.php?token=" . $token;
        $toEmail = $email;
        $subject = "User Registration Activation Email";
        $content = 
        "
            <html>
            <head>
              <title>Activation Email</title>
            </head>
            <body>
              <p>Please click on this link to activate your account</p>
              <p><a href='" . $actual_link . "'>" . $actual_link . "</a>
            </body>
            </html>
        ";
        $headers[] = 'MIME-Version: 1.0';
        $headers[] = 'Content-type: text/html; charset=iso-8859-1';
        $headers[] = 'From: VOMO';
        if(mail($toEmail, $subject, $content, implode("\r\n", $headers))) echo "Everything OK";
    }
}


else 
{
    echo "Incomplete Data";
}

// Close mySQL Connection
mysqli_close($link);

unset($_POST);

?>