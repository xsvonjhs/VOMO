<?php
require("connect.php");
        
$check_ID = $_POST["ID_frame"];
  

$sql = "SELECT Frame_1, Frame_2, Frame_3, Frame_4, Frame_5, Frame_6 FROM VOMO WHERE id = ".$check_ID;

$stmt = $link->prepare($sql);

if($stmt === false){
    http_response_code(404);
    die(mysqli_error($link));
}

if(!$stmt->execute()){
    die(mysqli_error($link));
}

$result = $stmt->get_result();

$row = mysqli_fetch_assoc($result);

$ReturnList = $row['Frame_1'].";".$row['Frame_2'].";".$row['Frame_3'].";".$row['Frame_4'].";".$row['Frame_5'].";".$row['Frame_6'];

$stmt->close();

echo $ReturnList;
        
	
?>