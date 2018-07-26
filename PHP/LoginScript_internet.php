<?php
require("connect.php");
      
$check_username = $_POST["check_usernamePost"]; 
$check_password = $_POST["check_passwordPost"]; 
  
$sql = "SELECT password, status FROM VOMO WHERE username = '$check_username'";
$stmt = $link->prepare($sql);
if($stmt === false){
    http_response_code(404);
    die(mysqli_error($link));
}

if(!$stmt->execute()){
    die(mysqli_error($link));
}
$result = $stmt->get_result();
$stmt->close();
            
if(mysqli_num_rows($result) > 0){
//show data for each row
    while($row = mysqli_fetch_assoc($result)){
		if ($row['password'] == $check_password && $row['status'] == 'ACTIVE'){
            echo "200;";                  
            $sql = "SELECT id FROM VOMO WHERE username = '$check_username'";
            $stmt = $link->prepare($sql);
            if($stmt === false){
                http_response_code(404);
                die(mysqli_error($link));
            }

            if(!$stmt->execute()){
                die(mysqli_error($link));
            }
            $IDresult = $stmt->get_result();
            $stmt->close();
            if(mysqli_num_rows($IDresult) > 0){
                if($row = mysqli_fetch_assoc($IDresult)) echo $row['id'];
                else echo "Error Occur";
            }
		}
		else if ($row['password'] == $check_password && $row['status'] == 'INACTIVE') echo "0000;";
		else echo "1139;";
	}
}
else{
    echo "404;";
}

?>