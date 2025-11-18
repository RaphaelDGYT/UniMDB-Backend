function fazPost(url, body, callback) {
  let request = new XMLHttpRequest();
  request.open("POST", url, true);
  request.setRequestHeader("Content-type", "application/json");
 
  request.onreadystatechange = function () {
    if (request.readyState === 4) {
      if (request.status === 200 || request.status === 201) {
        const resposta = JSON.parse(request.responseText);
        console.log("Resposta da API:", resposta);
        callback(resposta);
      } else {
        console.error("Erro:", request.status, request.responseText);
        callback(null);
      }
    }
  };
  request.send(JSON.stringify(body));
}
 
function loginUsuario(event) {
  event.preventDefault();
 
  const url = "https://probable-system-6jgj7qp5rwvc44jx-5254.app.github.dev/api/User/login";
  const email = document.getElementById("email").value;
  const passwordValue = document.getElementById("password").value;
 
  const body = {
    email: email,
    password: passwordValue
  };
 
  fazPost(url, body, function(resposta) {
    if (!resposta) {
      alert("Erro ao fazer login!");
      return;
    }
 
    // salvar token
    if (resposta.token) {
      salvarNoSessionStorage("token", resposta.token);
    }
 
    // salvar dados do usuário (não resposta.user)
    salvarNoSessionStorage("usuario", {
      id: resposta.id,
      name: resposta.name,
      username: resposta.username,
      email: resposta.email
    });
 
    window.location.href = "../index.html";
  });
}
 
function salvarNoSessionStorage(chave, valor) {
  try {
    const valorString = JSON.stringify(valor);
    sessionStorage.setItem(chave, valorString);
    console.log(`Dados salvos com sucesso: ${chave}`);
  } catch (erro) {
    console.error("Erro ao salvar no sessionStorage:", erro);
  }
}
 
function recuperarDoSessionStorage(chave) {
  try {
    const valorString = sessionStorage.getItem(chave);
    return JSON.parse(valorString);
  } catch (erro) {
    console.error("Erro ao recuperar do sessionStorage:", erro);
    return null;
  }
}
 
function removerDoSessionStorage(chave) {
  sessionStorage.removeItem(chave);
  console.log(`Dados removidos: ${chave}`);
}