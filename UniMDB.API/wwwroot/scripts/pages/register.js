const form = document.getElementById("form");
const password = document.getElementById("password");
const confPassword = document.getElementById("confPassword");

function ConfirmarSenha(item) {
  item.setCustomValidity("");
  item.checkValidity();

  if (item === confPassword) {
    if (item.value === password.value) {
      item.setCustomValidity("");
    } else {
      item.setCustomValidity("As senhas informadas não são correspondentes.");
    }
  }
}

function fazPost(url, body) {
  let request = new XMLHttpRequest();
  request.open("POST", url, true);
  request.setRequestHeader("Content-type", "application/json");
  request.send(JSON.stringify(body));

  request.onload = function () {
    if (this.status === 200 || this.status === 201) {
      alert("✅ Usuário cadastrado com sucesso!");
      window.location.href = "../pages/login.html";
    } else {
      alert(`Erro ao cadastrar: ${this.status}\n${this.responseText}`);
    }
  };

  request.onerror = function () {
    alert("Erro de conexão com o servidor.");
  };
}

function cadastraUsuario(event) {
  event.preventDefault(); 

  const url = "/api/User/add";
  const valorName = document.getElementById("name").value;
  const username = document.getElementById("username").value;
  const email = document.getElementById("email").value;
  const passwordValue = document.getElementById("password").value;

  const body = {
    name: valorName,
    username: username, 
    email: email,
    password: passwordValue,
  };

  fazPost(url, body);
}


