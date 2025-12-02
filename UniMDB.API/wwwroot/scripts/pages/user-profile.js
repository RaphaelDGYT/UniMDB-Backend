const usuario = recuperarDoSessionStorage("usuario");
document.getElementById("nome").textContent = usuario.name;
document.getElementById("username").textContent = usuario.username;
document.getElementById("email").textContent = usuario.email;

function favorites(event){
    window.location.href = `../pages/favorites.html`
}

async function loadFavorites() {
    try {
        const usuario = recuperarDoSessionStorage("usuario");
        const id = usuario.id;
        const url = `/api/User/GetAllFavorite/${id}`;
        const resultado = await fetch(url);
        const data = await resultado.json();
        const lista = document.getElementById("favorites");
        
        const limite = data.favorites.slice(0, 7);
        limite.forEach(async (fav) => {
            try {
                const urlFilme = `https://api.imdbapi.dev/titles/${fav.id_Movie_Mdb}`;
                const resultadoFilme = await fetch(urlFilme);
                const infoFilme = await resultadoFilme.json();

                let card = document.createElement('div');
                card.classList.add("card-filme");

                let img = document.createElement('img');
                img.src = infoFilme.primaryImage?.url;
                img.alt = infoFilme.originalTitle || "";
                img.classList.add("img-filme");

                card.appendChild(img);
                lista.appendChild(card);

                card.addEventListener('click', () => {
                    window.location.href = `../pages/movie-details.html?id=${infoFilme.id}`;
                });

            } catch (erroFilme) {
                console.error("Erro ao carregar um filme:", erroFilme);
            }
        });
    } catch (error) {
        throw error;
    }
}
loadFavorites();

function logout(){
    removerDoSessionStorage("token");
    window.location.href = "../index.html";
}

async function reviews() {
    try {
        const usuario = recuperarDoSessionStorage("usuario");
        const id = usuario.id;
        const url = `/api/User/getreviews/1?id=${id}`;
        const resultado = await fetch(url);
        const data = await resultado.json();
        const lista = document.getElementById("filmeReviews");

        const reviewsUnicos = [
            ...new Map(data.reviews.map(f => [f.movie_Id, f])).values()
        ];
        const limite = reviewsUnicos.slice(0, 7);
        limite.forEach(async (review) => {
            try {
                const urlFilme = `https://api.imdbapi.dev/titles/${review.movie_Id}`;
                const resultadoFilme = await fetch(urlFilme);
                const infoFilme = await resultadoFilme.json();
                let card = document.createElement('div');
                card.classList.add("card-filme");

                let img = document.createElement('img');
                img.src = infoFilme.primaryImage?.url || "../images/no-image.png";
                img.alt = infoFilme.originalTitle || "";
                img.classList.add("img-filme");
                let info = document.createElement('div');
                info.classList.add("info-filme");

                card.appendChild(img);
                card.appendChild(info);
 
                lista.appendChild(card);
                card.addEventListener('click', () => {
                    window.location.href = `../pages/movie-details.html?id=${infoFilme.id}`
                });
            }catch (error) {
                throw error;
            }
        });
    }catch (error) {
        throw error;
    }
}
reviews();