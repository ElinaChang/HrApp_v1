<?php 
    $host = "localhost";
    $dbuser = "root";
    $dbpassword = "1218";
    $dbname = "hrapp";
    $conn = new mysqli($host,$dbuser,$dbpassword,$dbname);
    if ($conn->connect_error)
    {
        die("Connection failed: " . $conn->connect_error);
    }

    if(isset($_POST["account"], $_POST["pwd"], $_POST["identify"]))
    {
        $account = trim($_POST["account"]);
        $pwd = trim($_POST["pwd"]);
        $identify = trim($_POST["identify"]);

        $stmt = $conn->prepare("SELECT * FROM staffinfo WHERE account = ? AND pwd = ? AND identify = ?");
        $stmt->bind_param("sss", $account, $pwd, $identify);
        if (!$stmt->execute()) 
        {
            die("Query Error: " . $stmt->error);
        }
        $result = $stmt->get_result();

        if ($result->num_rows > 0) 
        {
            $row = $result->fetch_assoc();

            /* session_start();

            $_SESSION['staff_id'] = $row['staff_id'];
            $_SESSION['account'] = $account;*/

            if ($identify == "使用者身份")
            {
                $staffID = $row["staff_id"];
                $login_Time = date("Y-m-d H:i:s");
                $insert_stmt = $conn->prepare("INSERT INTO log_record (log_staff_id, user_account, login_Time) VALUES (?, ?, ?)");
                $insert_stmt->bind_param("iss", $staffID, $account, $login_Time);
                $insert_stmt->execute();
            } 
            else if ($identify == "管理者身份")
            {
                $ma_account = $row["account"];
                $insert_stmt = $conn->prepare("INSERT INTO ma_record (ma_account) VALUES (?)");
                $insert_stmt->bind_param("s", $account);
                $insert_stmt->execute();
            }
            echo "logInformation inserted";

            /* header("Location: checkInfo.php");
            exit(); */
        } 
        else 
        {
            echo "logInformation failed"; 
        }
    } 
    else 
    {
        echo "請確認帳號、密碼、登入身份無誤";
    }

    $conn->close();

?>
