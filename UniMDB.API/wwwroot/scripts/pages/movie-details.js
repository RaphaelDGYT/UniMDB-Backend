const params = new URLSearchParams(window.location.search);
const id = params.get('id');

if (id) {
  fetch(`https://api.imdbapi.dev/titles/${id}`)
    .then(res => res.json())
    .then(filme => {
      document.getElementById('titulo').textContent = filme.primaryTitle || 'Título desconhecido';
      document.getElementById('poster').src = filme.primaryImage?.url || '';
      document.getElementById('poster').alt = filme.primaryTitle || '';
      document.getElementById('ano').textContent = filme.startYear || 'Desconhecido';
      document.getElementById('nota').textContent = filme.rating?.aggregateRating || 'Sem nota';
      document.getElementById('sinopse').textContent = filme.plot || 'Sinopse não disponível.';
    })
    
}

const btnAvaliar = document.getElementById("btnAvaliar");
const modal = document.getElementById("modal");
const fechar = document.getElementById("fechar");
const estrelasContainer = document.getElementById("estrelas");

let nota = 0;

// Criar 10 estrelas
for (let i = 1; i <= 10; i++) {
  const estrela = document.createElement("span");
  estrela.classList.add("star");
  estrela.innerHTML = "★";
  estrela.dataset.valor = i;

  estrela.addEventListener("click", () => {
    nota = i;
    atualizarEstrelas();
    console.log("Nota escolhida:", nota);
  });

  estrelasContainer.appendChild(estrela);
}

function atualizarEstrelas() {
  const estrelas = document.querySelectorAll(".star");
  estrelas.forEach(star => {
    star.classList.remove("selecionada");
    if (star.dataset.valor <= nota) {
      star.classList.add("selecionada");
    }
  });
}

// Abrir modal
btnAvaliar.onclick = () => {
  modal.style.display = "flex";
};

// Fechar modal X
fechar.onclick = () => {
  modal.style.display = "none";
};

// Fechar clicando fora
window.onclick = (e) => {
  if (e.target === modal) {
    modal.style.display = "none";
  }
};

//enviar avaliação

function fazPost(url, body) {
  let request = new XMLHttpRequest();
  request.open("POST", url, true);
  request.setRequestHeader("Content-type", "application/json");
  request.send(JSON.stringify(body));

  request.onload = function () {
    if (this.status === 200 || this.status === 201) {
      alert("Review enviada com sucesso!");
  
    } else {
      alert(`Erro ao enviar: ${this.status}\n${this.responseText}`);
    }
  };

  request.onerror = function () {
    alert("Erro de conexão com o servidor.");
  };
}

function cadastraReview(event) {
  event.preventDefault(); 

  const url = "https://probable-system-6jgj7qp5rwvc44jx-5254.app.github.dev/api/Review/add";
  const comment = document.getElementById("comment").value;
  const id_user = 15;
  const id_movie = id;
 

  const body = {
    score: nota,
    comment: comment, 
    id_User: id_user,
    id_Movie: id_movie,
  };

  fazPost(url, body);
}