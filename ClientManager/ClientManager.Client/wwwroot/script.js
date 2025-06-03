const apiUrl = "https://localhost:5001/api/clients";

document.getElementById("clientForm").addEventListener("submit", async (e) => {
    e.preventDefault();

    const name = document.getElementById("name").value;
    const address = document.getElementById("address").value;
    const nip = document.getElementById("nip").value;
    const additionalRaw = document.getElementById("additionalFields").value;

    const additional = {};
    additionalRaw.split("\n").forEach(line => {
        const [key, value] = line.split(":");
        if (key && value) additional[key.trim()] = value.trim();
    });

    await fetch(apiUrl, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ name, address, nip, additionalFields: additional })
    });

    loadClients();
});

async function loadClients() {
    const res = await fetch(apiUrl);
    const clients = await res.json();

    const tbody = document.querySelector("#clientTable tbody");
    tbody.innerHTML = "";

    clients.forEach(c => {
        const row = document.createElement("tr");
        const additional = c.additionalFields
            ? Object.entries(c.additionalFields).map(([k, v]) => `${k}: ${v}`).join("<br>")
            : "-";

        row.innerHTML = `
      <td>${c.name}</td>
      <td>${c.address}</td>
      <td>${c.nip}</td>
      <td>${additional}</td>
      <td>
        <button onclick="deleteClient('${c.id}')">❌</button>
      </td>
    `;
        tbody.appendChild(row);
    });
}

async function deleteClient(id) {
    await fetch(`${apiUrl}/${id}`, { method: "DELETE" });
    loadClients();
}

function downloadReport() {
    window.open("https://localhost:5001/report", "_blank");
}

loadClients();