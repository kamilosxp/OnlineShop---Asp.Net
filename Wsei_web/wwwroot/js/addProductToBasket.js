async function addProduct(prodId) {
    const basket = document.getElementById("koszyk");

    const product = {
        id: prodId
    };
    const id = prodId;
    console.log(prodId);
    console.log(JSON.stringify(product));

    const response = await fetch("/Cart/Buy",
        {
            method: "POST",
            headers: { "Content-type": "application/json" },
            body: JSON.stringify(product)
        });

    if (response.ok) {
        basket.style.fontSize = '1.5rem';
        setTimeout(() => {
            basket.style.fontSize = "1rem";
        },
            2000);

    }
};