const uri = 'api/Songs'; 
let songs = [];

function getSongs() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displaySongs(data))
        .catch(error => console.error('Unable to get songs.', error));
}

function addSong() {
    const addNameTextbox = document.getElementById('add-name');

    const song = {
        name: addNameTextbox.value.trim(), 
    };
    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(song)
    })
        .then(response => response.json())
        .then(() => {
            getSongs();
            addNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add song.', error));
}

function deleteSong(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getSongs())
        .catch(error => console.error('Unable to delete song.', error));
}

function displayEditForm(id) {
    const song = songs.find(song => song.id === id);
    document.getElementById('edit-id').value = song.id;
    document.getElementById('edit-name').value = song.name; 
    document.getElementById('editForm').style.display = 'block';
}

function updateSong() {
    const songId = document.getElementById('edit-id').value;
    const song = {
        id: parseInt(songId, 10),
        name: document.getElementById('edit-name').value.trim(), 
    };
    fetch(`${uri}/${songId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(song)
    })
        .then(response => response.json())
        .then(() => {
            getSongs();
        })
        .catch(error => console.error('Unable to update song.', error));
}

function _displaySongs(data) {
    const tBody = document.getElementById('songs'); 
    tBody.innerHTML = '';

    const button = document.createElement('button');

    data.forEach(song => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${song.id})`);
        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteSong(${song.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(song.name); 
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        td2.appendChild(editButton);

        let td3 = tr.insertCell(2);
        td3.appendChild(deleteButton);
    });

    songs = data;
}
