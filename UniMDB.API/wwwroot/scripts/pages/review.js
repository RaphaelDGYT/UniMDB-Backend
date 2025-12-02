async function reviews() {
    try {
        const usuario = recuperarDoSessionStorage("usuario");
        const id = usuario.id;
        const url = `/api/User/getreviews/1?id=${id}`;
        const resultado = await fetch(url);
        const data = await resultado.json();
        const lista = document.getElementById("filmeReview");
        data.reviews.forEach(async (review) => {
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

                let titulo = document.createElement('h3');
                titulo.textContent = infoFilme.primaryTitle;

                let reviewText = document.createElement('p');
                reviewText.classList.add("texto-review");
                reviewText.textContent = review.comment || "Sem comentário.";
 
                let reviewScore = document.createElement('p');
                reviewScore.classList.add("nota-review");
                reviewScore.textContent = review.score || "Sem nota.";

                let reviewDeleteBtn = document.createElement('button');
                reviewDeleteBtn.classList.add("deletar-review");
                reviewDeleteBtn.textContent ="Deletar review";

                reviewDeleteBtn.onclick = (e) => {
                    e.stopPropagation();
                    deleteReview(review.id);
                };

                let info = document.createElement('div');
                info.classList.add("info-filme");
 
                info.appendChild(titulo);
                info.appendChild(reviewScore);
                info.appendChild(reviewText);
                info.appendChild(reviewDeleteBtn);
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



async function deleteReview(idReview) {
    const url = `/api/Review/delete/${idReview}`;
    const resposta = await fetch(url, {
        method: "DELETE"
    });
        alert("Review deletada!");
        location.reload();
} 

reviews();