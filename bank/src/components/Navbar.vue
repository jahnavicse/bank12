<template>
  <nav class="navbar">

    <!-- LOGO -->
    <h2
      class="logo"
      @click="goHome"
    >
      🏦 My Bank
    </h2>

    <!-- MOBILE BUTTON -->
    <button
      class="menu-toggle"
      @click="toggleMenu"
    >
      ☰
    </button>

    <!-- MENU -->
    <ul :class="{ active: isOpen }">

      <!-- AFTER LOGIN -->
      <template v-if="userStore.isLoggedIn">

        <li>
          <router-link to="/dashboard">
            Dashboard
          </router-link>
        </li>

        <li>
          <router-link to="/profile">
            Profile
          </router-link>
        </li>

        <li>
          <router-link to="/transactions">
            Transactions
          </router-link>
        </li>

        <li>
          <button @click="logout">
            Logout
          </button>
        </li>

      </template>

      <!-- BEFORE LOGIN -->
      <template v-else>

        <li>
          <router-link to="/login">
            Login
          </router-link>
        </li>

      </template>

    </ul>

  </nav>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useUserStore } from '../store/user'

const router = useRouter()
const userStore = useUserStore()

const isOpen = ref(false)

// LOGOUT
function logout() {

  userStore.logout()

  router.push('/')

}

// MOBILE MENU
function toggleMenu() {

  isOpen.value = !isOpen.value

}

// GO HOME
function goHome() {

  router.push('/')

}
</script>

<style scoped>

.navbar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  background: #2c3e50;
  color: white;
  padding: 14px 24px;
  position: relative;
}

/* LOGO */
.logo {
  margin: 0;
  cursor: pointer;
  user-select: none;
}

/* MENU */
ul {
  display: flex;
  gap: 20px;
  list-style: none;
  margin: 0;
  padding: 0;
}

/* LINKS */
a {
  color: white;
  text-decoration: none;
  font-weight: bold;
}

a.router-link-active {
  color: #42b983;
}

/* BUTTON */
button {
  background: transparent;
  border: none;
  color: white;
  cursor: pointer;
  font-weight: bold;
  font-size: 15px;
}

/* MOBILE BUTTON */
.menu-toggle {
  display: none;
  font-size: 24px;
}

/* MOBILE */
@media (max-width: 768px) {

  .menu-toggle {
    display: block;
  }

  ul {
    display: none;
    flex-direction: column;
    position: absolute;
    top: 60px;
    right: 10px;
    background: #34495e;
    padding: 15px;
    border-radius: 8px;
    width: 180px;
  }

  ul.active {
    display: flex;
  }
}

</style>