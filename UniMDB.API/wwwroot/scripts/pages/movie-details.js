const params = new URLSearchParams(window.location.search);
const id = params.get('id');
const usuario = recuperarDoSessionStorage("usuario");
let idFavorito = "";

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

btnAvaliar.onclick = () => {
  modal.style.display = "flex";
  
};
fechar.onclick = () => {
  modal.style.display = "none";
};
window.onclick = (e) => {
  if (e.target === modal) {
    modal.style.display = "none";
  }
};

function fazPost(url, body) {
  let request = new XMLHttpRequest();
  request.open("POST", url, true);
  request.setRequestHeader("Content-type", "application/json");
  request.send(JSON.stringify(body));
  request.onload = function () {
    if (this.status === 200 || this.status === 201) {
      alert("Informação enviada com sucesso!");

    } else {
      alert(`Erro ao enviar: ${this.status}\n${this.responseText}`);
    }
  };
  request.onerror = function () {
    alert("Erro de conexão com o servidor.");
  };
  setTimeout(() => {
  location.reload();
  }, 2000);
   
}

function saveReview(event) {
  event.preventDefault(); 
  const url = "/api/Review/add";
  const comment = document.getElementById("comment").value;
  const id_user = usuario.id;
  const id_movie = id;
  const body = {
    score: nota,
    comment: comment, 
    id_User: id_user,
    id_Movie: id_movie,
  };
  fazPost(url, body);
  
}

async function favorite(event) {
    event.preventDefault();
    const btn = document.getElementById("favoriteBtn");
    const id_user = usuario.id;

    if (btn.dataset.favorite === "1") {
        if (idFavorito) {
            await fetch(`/api/Favorite/delete/${idFavorito}`, {
                method: "DELETE"
            });
            btn.dataset.favorite = "0";
            btn.textContent = "♡";
            idFavorito = null;
        }
        return;
    }

    const url = `/api/Favorite/add`;
    const body = {
        id_User: id_user,
        id_Movie_Mdb: id
    };

    const res = await fetch(url, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(body)
    });

    const favoritoCriado = await res.json();
    idFavorito = favoritoCriado.id_Favorite;
    btn.dataset.favorite = "1";
    btn.textContent = "♥";
    
}

async function verifyFavorite() {
  const userId = usuario.id;
  const url = `/api/User/GetAllFavorite/${userId}`;
  const res = await fetch(url);
  const data = await res.json();

  const favorito = data.favorites.find(f => f.id_Movie_Mdb === id);

  if (favorito) {
    idFavorito = favorito.id_Favorite; 
    const btn = document.getElementById("favoriteBtn");
    btn.dataset.favorite = "1";
    btn.textContent = "♥";
  }
}
verifyFavorite();


async function otherReviews() {
    try {
        const url = `/api/Review/getall/${id}`;
        const resultado = await fetch(url);
        const reviews = await resultado.json();
        const container = document.getElementById("filmeReview");
        container.innerHTML = "";

        reviews.reviews.forEach(review => {
            let card = document.createElement("div");
            card.classList.add("card-filme");

            let reviewUser = document.createElement("h3");
            reviewUser.textContent = review.username_User

            let reviewText = document.createElement("p");
            reviewText.classList.add("texto-review");
            reviewText.textContent = review.comment || "Sem comentário.";

            let reviewScore = document.createElement("p");
            reviewScore.classList.add("nota-review");
            reviewScore.textContent = review.score || "Sem nota.";

            let info = document.createElement("div");
            info.classList.add("info-filme");

            info.appendChild(reviewUser);
            info.appendChild(reviewScore);
            info.appendChild(reviewText);
            card.appendChild(info);
            container.appendChild(card);
        });  
    } catch (error) {
        throw error;
    }
}
otherReviews();

async function relatedsMovies(id) {
    const container = document.getElementById("relateds");
    container.innerHTML = ""; 

    const url_filme = `https://api.imdbapi.dev/titles/${id}`;
    const info = await fetch(url_filme).then(resultado => resultado.json());
    
    let interests = "";
    if (Array.isArray(info.interests)) {
        info.interests.forEach(i => {
            interests += `&interestIds=${i.id}`;
        });
    } 

    const url_relacionados = `https://api.imdbapi.dev/titles?types=MOVIE${interests}&sortBy=SORT_BY_POPULARITY`;
    const data = await fetch(url_relacionados).then(r => r.json());
    const filmes = (data.titles || []).slice(0, 7);

    filmes.forEach(filme => {
        let elemento = document.createElement("img");
        elemento.src = filme.primaryImage.url;
        elemento.alt = filme.originalTitle;
        elemento.title = filme.primaryTitle;

        container.appendChild(elemento);

        elemento.addEventListener("click", () => {
            window.location.href = `/pages/movie-details.html?id=${filme.id}`;
        });
    });
}
relatedsMovies(id);
