<script>
export default {
  name: "app",
  title: "D'Taquito: Find a field near ur home",
  data() {
    return {
      drawer: false,
      pages: [
        { label: "Home", to: "/home" },
        { label: "About", to: "/about" },
        { label: "SportSpaces", to: "/fields" },
        { label: "Login", to: "/login" },
        { label: "Register", to: "/register" },
        { label: "My Reserve", to: "/myreserve", role: "1" },
        { label: "Payment", to: "/payment"},
        {label: "Subscription", to: "/subscription"}
      ],
      isLoggedIn: false,
      userRole: null,
    }
  },
  created() {
    this.isLoggedIn = !!localStorage.getItem('userId');
    this.userRole = localStorage.getItem('userRole');
  },
  methods: {
    toggleDrawer() {
      this.drawer = !this.drawer;
      },
    logout() {
      localStorage.removeItem('userId');
      localStorage.removeItem('userRole');
      this.isLoggedIn = false;
      this.userRole = null;
      this.$router.push("/login");
    },
  }
}
</script>

<template>
  <header>
  <pv-toolbar class="toolbar-layout">
    <template #start>
      <pv-button class="p-button-text text-white" icon="pi pi-bars" @click="toggleDrawer"></pv-button>
      <h2> D'Taquito </h2>
    </template>
    <template #end>
      <div class="flex-column">
        <router-link v-for="page in pages" :key="page.label" v-slot="{navigate, href}" :to="page.to" custom>
          <pv-button :href="href" class="p-button-text text-white" @click="navigate"
                     v-if="((page.label !== 'My Reserve' && page.label !== 'Payment' && page.label !== 'Subscription') || isLoggedIn) &&
          ((page.label === 'Login' || page.label === 'Register' ) && !isLoggedIn ||
          (page.label !== 'Login' && page.label !== 'Register' &&
          (!page.role || page.role === userRole)))">
            {{ page.label }}
          </pv-button>
        </router-link>
        <pv-button class="p-button-text text-white" @click="logout" v-if="isLoggedIn">
          Logout
        </pv-button>
      </div>
    </template>
  </pv-toolbar>
  </header>
  <router-view/>
</template>

<style scoped>
.toolbar-layout {
  display: flex;

  top: 0;
  left: 0;
  right: 0;
  background-color: darkgreen;
  padding: 0 1rem;
}

</style>
