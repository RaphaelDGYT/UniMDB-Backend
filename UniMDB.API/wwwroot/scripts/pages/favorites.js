async function favorites() {
    try {
        const usuario = recuperarDoSessionStorage("usuario");
        const id = usuario.id;
        const url = `/api/User/GetAllFavorite/${id}`;
        const resultado = await fetch(url);
        const data = await resultado.json();
        
        if (!data.favorites || data.favorites.length === 0) {
            console.error("Nenhum favorito encontrado!");
            return;
        }
        const lista = document.getElementById("favorites");
        const favoritosUnicos = [
            ...new Map(data.favorites.map(f => [f.id_Movie_Mdb, f])).values()
        ];
        favoritosUnicos.forEach(async (favorites) => {
            try {
                const urlFilme = `https://api.imdbapi.dev/titles/${favorites.id_Movie_Mdb}`;
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
            
            } 
            
            catch (erroFilme) {
                console.error("Erro ao carregar um filme:", erroFilme);
            }
        });

    } catch (error) {
        throw error;
    }
}


favorites();
