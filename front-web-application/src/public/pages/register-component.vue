<script>
import { UserApiService } from "@/dtaquito/services/user.api.service.js";
import {Subscription} from "@/dtaquito/model/subscription.entity.js";
import {SubscriptionApiService} from "@/dtaquito/services/subscription.api.service.js";

export default {
  name: "register",
  title: "Register",
  data() {
    return {
      name: "",
      roleId: "",
      email: "",
      password: "",
      confirmPassword: "",
    };
  },
  methods: {
    async register() {
      const userService = new UserApiService();


      const user = {
        name: this.name,
        roleId: this.roleId,
        email: this.email,
        password: this.password,
      };


      if (this.password !== this.confirmPassword) {
        alert("Las contraseñas no coinciden");
        return;
      }

      try {
        console.log(user);
        await userService.create(user);

        alert("Usuario registrado con éxito");
        this.$router.push("/login");
      } catch (error) {
        console.error("Error al registrar el usuario: ", error);
      }
    },
  },
};
</script>

<template>
  <div class="register">
    <h1>Registro</h1>
    <form @submit.prevent="register">
      <div class="form-group">
        <label for="name">Name</label>
        <input type="text" id="name" name="name" v-model="name" required>
      </div>
      <div class="form-group">
        <label for="role">Rol</label>
        <select id="role" name="role" v-model="roleId" required>
          <option value="1">Rentor</option>
          <option value="2">Propietario</option>
        </select>
      </div>
      <div class="form-group">
        <label for="email">Email</label>
        <input type="email" id="email" name="email" v-model="email" required>
      </div>
      <div class="form-group">
        <label for="password">Password</label>
        <input type="password" id="password" name="password" v-model="password" required>
      </div>
      <div class="form-group">
        <label for="confirm-password">Confirm Password</label>
        <input type="password" id="confirm-password" name="confirm-password" v-model="confirmPassword" required>
      </div>
      <button type="submit">Registrar</button>
    </form>
  </div>
</template>

<style scoped>
.register {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  height: 100vh;
}

.register form {
  display: flex;
  flex-direction: column;
  width: 300px;
  padding: 20px;
  border-radius: 5px;
  box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
}

.register form .form-group {
  display: flex;
  flex-direction: column;
  margin-bottom: 15px;
}

.register form .form-group label {
  margin-bottom: 5px;
}

.register form .form-group input,
.register form .form-group select {
  padding: 10px;
  border-radius: 5px;
  border: 1px solid #ccc;
}

.register form button {
  padding: 10px;
  border-radius: 5px;
  border: none;
  background-color: forestgreen;
  color: white;
  cursor: pointer;
}

.register form button:hover {
  background-color: goldenrod;
}
</style>