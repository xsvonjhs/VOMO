<?php
require("connect.php");

$token = $_GET["token"];

if(isset($token)) {
    $sql = "UPDATE VOMO set status = 'ACTIVE' WHERE token='$token'";
    $stmt = $link->prepare($sql);
    if($stmt === false){
        http_response_code(404);
        die(mysqli_error($link));
    }

    if(!$stmt->execute()){
        die(mysqli_error($link));
    }
    $result = $link->affected_rows;
    $stmt->close();
	if($result==1) {
		echo "Your account is activated.";
	} else {
		echo "Problem in account activation.";
	}
}
?>