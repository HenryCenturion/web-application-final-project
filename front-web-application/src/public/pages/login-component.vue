<script>
import { UserApiService } from "@/dtaquito/services/user.api.service.js";


export default {
  name: "login",
  title: "Login",
  data() {
    return {
      username: "",
      password: "",
      error: "",
    };
  },
  methods: {
    async login() {
        console.log("Inicio del método login"); // Nuevo console.log

        const userService = new UserApiService();
        const users = await userService.getAll(this.username, this.password );
        const user = users.data;

        console.log("user: ", users.data);


        //const user = users.data.find(u => u.email === this.username && u.password === this.password);

        if (user) {
          // Usuario autenticado, redirigir a la página de inicio
          console.log("Inicio de sesión exitoso");
          localStorage.setItem('userRole', user.roleId);
          localStorage.setItem('userId', user.id);
          localStorage.setItem('isLoggedIn', 'true'); // Guardar booleano
          this.$router.push("/fields");
        } else {
          // Credenciales incorrectas
          this.error = "Nombre de usuario o contraseña incorrectos";
        }
    },
  },
};
</script>

<template>
  <div class="login">
    <h1>Login</h1>
    <form @submit.prevent="login">
      <div class="form-group">
        <label for="username">Username</label>
        <input type="text" id="username" name="username" v-model="username" required>
      </div>
      <div class="form-group">
        <label for="password">Password</label>
        <input type="password" id="password" name="password" v-model="password" required>
      </div>
      <button type="submit">Login</button>
      <p v-if="error">{{ error }}</p>
    </form>
  </div>
</template>

<style scoped>
.login {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  height: 100vh;
}

.login form {
  display: flex;
  flex-direction: column;
  width: 300px;
  padding: 20px;
  border-radius: 5px;
  box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
}

.login form .form-group {
  display: flex;
  flex-direction: column;
  margin-bottom: 15px;
}

.login form .form-group label {
  margin-bottom: 5px;
}

.login form .form-group input {
  padding: 10px;
  border-radius: 5px;
  border: 1px solid #ccc;
}

.login form button {
  padding: 10px;
  border-radius: 5px;
  border: none;
  background-color: forestgreen;
  color: white;
  cursor: pointer;
}

.login form button:hover {
  background-color: goldenrod;
}
</style>