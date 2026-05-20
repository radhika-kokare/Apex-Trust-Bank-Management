
const apiBase = "https://localhost:7001/api/Accounts";

function generateAccountNumber() {
    return "AC" + Math.floor(100000000 + Math.random() * 900000000);
}

function createAccount() {

    var data = {
        FullName: $("#fullName").val(),
        Email: $("#email").val(),
        Mobile: $("#mobile").val(),
        Address: $("#address").val(),
        AccountType: $("#accountType").val(),
        AccountNumber: generateAccountNumber(), 
        Balance: 0                               
    };

    if (!data.FullName || !data.Email || !data.Mobile || !data.Address || !data.AccountType) {
        alert("All fields are required");
        return;
    }

    $.ajax({
        url: apiBase,
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(data),
        success: function () {
            alert("✅ Account Created Successfully");
            $("#createAccountForm")[0].reset();
        },
        error: function (err) {
            console.log("API Error:", err.responseText);
            alert("❌ Account Creation Failed");
        }
    });
}



function loadAccounts() {

    $.ajax({
        url: "https://localhost:7001/api/Accounts",
        type: "GET",
        success: function (data) {

            console.log("API Data:", data);

            var rows = "";

            if (!data || data.length === 0) {
                $("#accountTableBody").html(
                    "<tr><td colspan='6'>No records found</td></tr>"
                );
                return;
            }

            $.each(data, function (i, item) {
                rows += `
                    <tr>
                        <td>${item.accountNumber}</td>
                        <td>${item.fullName}</td>
                        <td>${item.email}</td>
                        <td>${item.mobile}</td>
                        <td>${item.accountType}</td>
                        <td>${item.balance}</td>
                    </tr>`;
            });

            $("#accountTableBody").html(rows);
        },
        error: function (err) {
            console.log("Error:", err);
            alert("Failed to load accounts");
        }
    });
}

