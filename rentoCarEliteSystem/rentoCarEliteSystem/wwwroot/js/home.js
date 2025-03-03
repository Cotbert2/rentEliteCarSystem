const currentSession = JSON.parse(localStorage.getItem('currentSession'));
console.log('currentSession', currentSession);
document.getElementById('name').innerText = `Bienvenido ${currentSession.firstName} ${currentSession.lastName}`;