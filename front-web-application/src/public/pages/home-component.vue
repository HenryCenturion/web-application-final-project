<script>
export default {
  name: "home",
  title: "Home"
}
</script>

<template>
  <div class="home">
    <h1>D'taquito:   ¡Reserva tu jugada, al instante y sin complicaciones!!</h1>
    <img class="logo" src="/src/assets/images/logo.ico" alt="123">
  </div>
</template>

<style scoped>
@media (min-width: 1024px) {
  .home {
    min-height: 100vh;
    display: flex;
    justify-content: center;
    align-items: center;

  }
}
.home{
  text-align: left;
  margin-top: 20px;
  font-size: 30px;
  font-weight: bold;
  color: whitesmoke;
  padding: 20px;
  border-radius:10px;
}
.logo{
  width: 50%;
  height: 20%;
  margin-top: 0;
  margin-left: 20px;
}
</style>