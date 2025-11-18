const LIMITE_PESQUISA = 20
const LIMITE_FILMES_HOME = 7
const ANO_ATUAL = new Date().getFullYear()

const pesquisa = document.getElementById('barra-pesquisa');
const btn_pesquisa = document.getElementById('pesquisar')

const filmes_home = document.getElementById('itens-home')

const filmes_pesquisa = document.getElementById('content');
const filmes_novidades = document.getElementById('novidades')
const filmes_populares = document.getElementById('populares')
const filmes_destaques = document.getElementById('destaques')
const filmes_melhores = document.getElementById('melhores')

function fetchPesquisaFilmes(valor)
{
    const resultado = fetch(`https://api.imdbapi.dev/search/titles?query=${encodeURIComponent(valor)}&limit=${LIMITE_PESQUISA}`)
        .then(res => {
            filmes_home.style.display = 'none';  
            return res;
        })
     
    return resultado
}

// TODO: Fazer a parte do catch para cada um abaixo

// Novidades

try {
    const API_novidades = `https://api.imdbapi.dev/titles?types=MOVIE&startYear=${ANO_ATUAL}&endYear=${ANO_ATUAL}&minVoteCount=100&minAggregateRating=6&sortBy=SORT_BY_RELEASE_DATE`

    fetch(API_novidades)
        .then(resultado => resultado.json())
        .then(info => {

            let filmes = info.titles.slice(0, LIMITE_FILMES_HOME)
            
            filmes.forEach(filme => {
                
                let elemento = document.createElement('img')

                elemento.src = filme.primaryImage.url
                elemento.alt = filme.originalTitle
                elemento.title = filme.primaryTitle

                filmes_novidades.append(elemento)

                elemento.addEventListener('click', () => {
                    window.location.href = `../pages/movie-details.html?id=${filme.id}`;
                });
            });

        })
        .catch()
    
} catch (error) {
    Throw;
}

// Populares

try {
    
    const API_populares = `https://api.imdbapi.dev/titles?types=MOVIE&startYear=${ANO_ATUAL}&endYear=${ANO_ATUAL}&sortBy=SORT_BY_USER_RATING_COUNT&sortOrder=DESC`

    fetch(API_populares)
        .then(resultado => resultado.json())
        .then(info => {
            if (!info.titles) return;

            let filmes = info.titles.slice(0, LIMITE_FILMES_HOME)
            
            filmes.forEach(filme => {
                
                let elemento = document.createElement('img')

                elemento.src = filme.primaryImage.url
                elemento.alt = filme.originalTitle
                elemento.title = filme.primaryTitle

                filmes_populares.append(elemento)

                elemento.addEventListener('click', () => {
                    window.location.href = `../pages/movie-details.html?id=${filme.id}`;
                });
            });

        })
        .catch()
    
} catch (error) {
    Throw;
}

// Melhores

try {
    const API_melhores = `https://api.imdbapi.dev/titles?types=MOVIE&minVoteCount=100000&sortBy=SORT_BY_USER_RATING&sortOrder=DESC`

    fetch(API_melhores)
        .then(resultado => resultado.json())
        .then(info => {

            let filmes = info.titles.slice(0, LIMITE_FILMES_HOME)
            
            filmes.forEach(filme => {
                
                let elemento = document.createElement('img')

                elemento.src = filme.primaryImage.url
                elemento.alt = filme.originalTitle
                elemento.title = filme.primaryTitle

                filmes_melhores.append(elemento)
                
                elemento.addEventListener('click', () => {
                    window.location.href = `../pages/movie-details.html?id=${filme.id}`;
                });
            });

        })
        .catch()
    
} catch (error) {
    Throw;
}

// Destaques

try {
    const API_destaques = `https://api.imdbapi.dev/titles?types=MOVIE&startYear=${ANO_ATUAL}&endYear=${ANO_ATUAL}&sortBy=SORT_BY_POPULARITY&sortOrder=ASC`

    fetch(API_destaques)
        .then(resultado => resultado.json())
        .then(info => {

            let filmes = info.titles.slice(0, LIMITE_FILMES_HOME)
            
            filmes.forEach(filme => {
                
                let elemento = document.createElement('img')

                elemento.src = filme.primaryImage.url
                elemento.alt = filme.originalTitle
                elemento.title = filme.primaryTitle

                filmes_destaques.append(elemento)

                elemento.addEventListener('click', () => {
                    window.location.href = `../pages/movie-details.html?id=${filme.id}`;
                });

            });

        })
        .catch()
    
} catch (error) {
    Throw;
}


btn_pesquisa.addEventListener('click', async (event) => {
    event.preventDefault();

    filmes_pesquisa.innerHTML = ''

    const resposta = await fetchPesquisaFilmes(pesquisa.value);    

    resposta.json().then(infos => {
        
        let obras = infos.titles    
            .filter(obra => obra.type === 'movie')
            .sort((a,b) =>
            {
                if (a.rating == null)
                {
                    return 1;
                }

                if (b.rating == null)
                {
                    return -1;
                }

                return b.rating.voteCount - a.rating.voteCount
                
            })

        obras.forEach(filme => {

            let elemento = document.createElement('img')

            elemento.src = filme.primaryImage.url
            elemento.title = filme.primaryTitle
            elemento.alt = filme.originalTitle

            filmes_pesquisa.append(elemento)

            elemento.addEventListener('click', () => {
                window.location.href = `../pages/movie-details.html?id=${filme.id}`;
            });


        });
            
    });
    
});

