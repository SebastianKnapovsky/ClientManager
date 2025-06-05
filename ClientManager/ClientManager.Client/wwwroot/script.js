const base = location.hostname === "localhost"
    ? "http://localhost:5001"
    : "http://host.docker.internal:5001";

const apiUrl = `${base}/api/Clients`;
const reportUrl = `${base}/api/Report/download`;

document.addEventListener('DOMContentLoaded', () => {
    loadClients();
    document.getElementById('clientForm').addEventListener('submit', handleFormSubmit);
});

function showForm() {
    document.getElementById('clientForm').style.display = 'block';
    document.getElementById('showFormBtn').style.display = 'none';
}

function cancelForm() {
    document.getElementById('clientForm').reset();
    document.getElementById('additionalFields').innerHTML = '';
    document.getElementById('clientId').value = '';
    document.getElementById('clientForm').style.display = 'none';
    document.getElementById('showFormBtn').style.display = 'inline-block';
}

function addAdditionalFieldInput(name = '', value = '') {
    const container = document.createElement('div');
    container.className = 'field-pair';

    container.innerHTML = `
        <input type="text" placeholder="Field Name" value="${name}" class="field-name" required />
        <input type="text" placeholder="Field Value" value="${value}" class="field-value" required />
        <button type="button" onclick="this.parentElement.remove()">❌</button>
    `;

    document.getElementById('additionalFields').appendChild(container);
}

async function loadClients() {
    const res = await fetch(apiUrl);
    const clients = await res.json();
    const list = document.getElementById('clientList');
    list.innerHTML = '';

    clients.forEach(client => {
        const div = document.createElement('div');
        div.className = 'client-item';

        const additional = client.additionalFields?.map(f =>
            `<strong>${f.nameField}:</strong> ${f.value}`
        ).join('<br/>') || '';

        div.innerHTML = `
            <strong>${client.name}</strong><br/>
            ${client.address}<br/>
            NIP: ${client.nip}<br/>
            ${additional}
            <br/>
            <button onclick='editClient(${JSON.stringify(client)})'>✏️ Edit</button>
            <button class="delete" onclick='deleteClient("${client.id}")'>🗑️ Delete</button>
        `;
        list.appendChild(div);
    });
}

async function handleFormSubmit(e) {
    e.preventDefault();

    const id = document.getElementById('clientId').value;
    const name = document.getElementById('name').value;
    const address = document.getElementById('address').value;
    const nip = document.getElementById('nip').value;

    const additionalFields = Array.from(document.querySelectorAll('.field-pair')).map(pair => ({
        nameField: pair.querySelector('.field-name').value,
        value: pair.querySelector('.field-value').value
    }));

    const clientData = { name, address, nip, additionalFields };

    if (id) {
        clientData.id = id;
        await fetch(`${apiUrl}/${id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(clientData)
        });
    } else {
        await fetch(apiUrl, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(clientData)
        });
    }

    cancelForm();
    loadClients();
}

function editClient(client) {
    showForm();
    document.getElementById('clientId').value = client.id;
    document.getElementById('name').value = client.name;
    document.getElementById('address').value = client.address;
    document.getElementById('nip').value = client.nip;
    document.getElementById('additionalFields').innerHTML = '';

    (client.additionalFields || []).forEach(f =>
        addAdditionalFieldInput(f.nameField, f.value)
    );
}

async function deleteClient(id) {
    await fetch(`${apiUrl}/${id}`, { method: 'DELETE' });
    loadClients();
}

async function generateReport() {
    const res = await fetch(reportUrl);
    const blob = await res.blob();
    const url = URL.createObjectURL(blob);

    const a = document.createElement('a');
    a.href = url;
    a.download = 'ClientReport.pdf';
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    URL.revokeObjectURL(url);
}