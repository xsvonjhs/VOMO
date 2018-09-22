<?php

require("connect.php");

$options = 1;

$sql = "SELECT * FROM VOMO";
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


query_to_csv($result, "vomoexport.csv", true);

function query_to_csv($result, $filename, $attachment = false, $headers = true) {        
    if($attachment) {
        // send response headers to the browser
        header( 'Content-Type: text/csv' );
        header( 'Content-Disposition: attachment;filename='.$filename);
        $fp = fopen('php://output', 'w');
    } else {
        $fp = fopen($filename, 'w');
    }
    
    if($headers) {
        // output header row (if at least one row exists)
        $row = mysqli_fetch_assoc($result);
        if($row) {
            fputcsv($fp, array_keys($row));
            // reset pointer back to beginning
            mysqli_data_seek($result, 0);
        }
    }
    
    while($row = mysqli_fetch_assoc($result)) {
        fputcsv($fp, $row);
    }
    
    fclose($fp);
}

?>