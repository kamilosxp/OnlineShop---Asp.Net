(function () {
    const alertElement = document.getElementById("success-alert");
    const formElement = document.forms[1];
    const addNewItem = async () => {
        const titleValue = document.getElementById("Title").value;
        const descriptionValue = document.getElementById("Description").value;
        const unitPriceValue = document.getElementById("UnitPrice").value;

        const productModel = {
            Title: titleValue,
            Description: descriptionValue,
            UnitPrice: unitPriceValue
        };

        const response = await fetch("/Admin/Create",
            {
                method: "POST",
                headers: { "Content-type": "application/json" },
                body: JSON.stringify(productModel)
            });

        if (response.ok) {
            alertElement.style.display = "block";
            setTimeout(() => {
                alertElement.style.display = "none";
            },
                2000);

        }
    };
    window.addEventListener("load", () => {
        formElement.addEventListener("submit", event => {
            event.preventDefault();
            addNewItem().then(() => console.log("added successfully"));
        });
    });
})();
