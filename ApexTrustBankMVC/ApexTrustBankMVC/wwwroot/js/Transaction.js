const txnApi = "https://localhost:7001/api/Transactions";
console.log("transaction.js LOADED");
/*alert("transaction.js LOADED");*/

function depositAmount() {
    alert("Deposit button CLICKED");  

    var data = {
        AccountNumber: $("#AccountNumber").val(),
        Amount: parseFloat($("#Amount").val())
    };

    $.ajax({
        url: "https://localhost:7001/api/Transactions/Deposit",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(data),
        success: function () {
            alert("Deposit successful");
        },
        error: function (xhr) {
            alert("Deposit failed");
            console.log(xhr.responseText);
        }
    });
}

function withdrawAmount() {

    alert("Withdraw CLICKED");

    var data = {
        AccountNumber: $("#AccountNumber").val(),
        Amount: parseFloat($("#Amount").val())
    };

    if (!data.AccountNumber || isNaN(data.Amount)) {
        alert("Please fill all fields");
        return;
    }

    $.ajax({
        url: "https://localhost:7001/api/Transactions/Withdraw",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(data),
        success: function (res) {
            alert(res.message);
        },
        error: function (xhr) {
            alert("Withdraw failed");
            console.log(xhr.responseText);
        }
    });
}


function transferAmount() {

    alert("Transfer CLICKED");

    var data = {
        FromAccountNumber: $("#fromAccount").val(),
        ToAccountNumber: $("#toAccount").val(),
        Amount: parseFloat($("#amount").val()) 
    };

    if (!data.FromAccountNumber || !data.ToAccountNumber || isNaN(data.Amount)) {
        alert("Please fill all fields correctly");
        return;
    }

    $.ajax({
        url: "https://localhost:7001/api/Transactions/Transfer",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(data),
        success: function (res) {
            alert(res.message || "Transfer Successful");
        },
        error: function (xhr) {
            alert("Transfer Failed");
            console.log(xhr.responseText);
        }
    });
}


$(document).ready(function () {

    if ($("#historyTable").length > 0) {
        loadTransactionHistory();
    }
});

function loadTransactionHistory() {

    $.ajax({
        url: txnApi + "/GetAll",
        type: "GET",
        success: function (data) {

            console.log("Transaction API Data:", data);

            let rows = "";

            if (!data || data.length === 0) {
                $("#historyTable tbody").html(
                    "<tr><td colspan='5'>No transaction records found</td></tr>"
                );
                return;
            }

            $.each(data, function (i, item) {
                rows += `
                    <tr>
                        <td>${formatDate(item.transactionDate)}</td>
                        <td>${item.accountNumber}</td>
                        <td>${item.transactionType}</td>
                        <td>${item.amount}</td>
                        <td>${item.balance}</td>
                    </tr>
                `;
            });

            $("#historyTable tbody").html(rows);
        },
        error: function (xhr) {
            console.log(xhr.responseText);
            alert("Failed to load transaction history");
        }
    });
}

function formatDate(dateString) {
    if (!dateString) return "";
    const d = new Date(dateString);
    return d.toLocaleDateString() + " " + d.toLocaleTimeString();
}

