const token = sessionStorage.getItem("token");
const headerComLogin = document.getElementById("headerComLogin");
const headerSemLogin = document.getElementById("headerSemLogin");

if (!token) {
    headerComLogin.style.display = "none";
    headerSemLogin.style.display = "flex";
} else {
    headerComLogin.style.display = "flex";
    headerSemLogin.style.display = "none";
}