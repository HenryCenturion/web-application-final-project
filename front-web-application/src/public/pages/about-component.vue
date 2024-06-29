<script>
export default {
  name: "about",
  title: "About Us"
}
</script>

<template>
  <div class="about">
    <h1>About Us: </h1>
    <div class="card-container">
      <div class="card">
        <h2>Acerca de D'taquito</h2>
        <p>Bienvenido a D'taquito, tu solución integral para la gestión de reservas de espacios deportivos de fútbol y mesas de billar en Lima Metropolitana. En D'taquito, nos apasiona facilitar el acceso a instalaciones deportivas y de entretenimiento, brindando a nuestros usuarios una experiencia fluida y conveniente para reservar y disfrutar de sus actividades favoritas.</p>
      </div>
      <div class="card">
        <h2>Nuestra misión</h2>
        <p>En D'taquito, nos esforzamos por simplificar la forma en que las personas acceden a espacios deportivos y de entretenimiento. Nuestra misión es proporcionar una plataforma intuitiva y fácil de usar que permita a los usuarios encontrar, reservar y disfrutar de instalaciones deportivas y recreativas en Lima Metropolitana con total comodidad y confianza.</p>
      </div>
      <div class="card">
        <h2>Nuestros Servicios</h2>
        <li>Reservas de Espacios Deportivos: Ya sea que estés buscando una cancha de fútbol para un partido con amigos o un campo de fútbol sala para entrenar, D'taquito te ofrece una amplia variedad de opciones para satisfacer tus necesidades deportivas.</li>
        <br>
        <li>Reservas de Mesas de Billar: ¿Te apetece una partida de billar con amigos? Con D'taquito, puedes reservar fácilmente mesas de billar en los mejores locales de Lima Metropolitana y pasar un buen rato en un ambiente relajado.</li>
      </div>
      <div class="card">
        <h2>Nuestro Compromiso</h2>
        <p>En D'taquito, nos comprometemos a proporcionar un servicio de calidad excepcional a nuestros usuarios. Trabajamos en estrecha colaboración con instalaciones deportivas y recreativas de confianza en toda Lima Metropolitana para garantizar que cada reserva sea una experiencia sin problemas y satisfactoria para nuestros clientes.</p>
      </div>
      <div class="card">
        <h2>Contáctanos</h2>
        <p>¿Tienes alguna pregunta, comentario o sugerencia? No dudes en ponerte en contacto con nosotros. Estamos aquí para ayudarte en cualquier momento.</p>
        <li>Correo Electrónico: info@dtaquito.com</li>
        <li>Teléfono: +51 950 456 789</li>
        <li>Redes Sociales: Facebook | Twitter | Instagram</li>
        <p>¡Gracias por elegir D'taquito para tus necesidades de reserva de espacios deportivos y mesas de billar en Lima Metropolitana!</p>
      </div>
    </div>
  </div>
</template>

<style scoped>
@media (min-width: 1024px){
  .about {
    min-height: 100vh;
    justify-content: flex-start;
    align-items: flex-start;
    color: whitesmoke;
    padding-top: 0;
    margin-top: 0;
    top:0;
    text-align: left;
  }
}

.about h1 {
  color: forestgreen;
  font-size: 2em;
}

.about h2 {
  color: #ffffff;
  margin-bottom: 10px;
}

.card {
  background-color: #1e1e1e; /* Color de fondo de la tarjeta */
  border-radius: 5px; /* Bordes redondeados de la tarjeta */
  padding: 20px; /* Espacio interior de la tarjeta */
  margin-bottom: 20px; /* Espacio debajo de la tarjeta */
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1); /* Sombra alrededor de la tarjeta */
  width: calc(50% - 40px); /* Ancho de la tarjeta */
  margin: 20px;
}
.card-container {
  display: flex;
  flex-wrap: wrap;
}
</style>