<?php

$host = "localhost"; //put your host here
$user = "id6520240_vomoapps"; //in general is root
$password = "123456"; //use your password here
$dbname = "id6520240_vomoapps"; //your database


$link = new mysqli($host, $user, $password, $dbname);

if ($link->connect_error) die("Connection failed: " . $link->connect_error);

?>