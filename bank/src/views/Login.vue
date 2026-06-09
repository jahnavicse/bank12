<template>
  <div class="login-container">

    <div class="login-card">

      <!-- Logo -->
      <h1>🏦 My Bank</h1>

      <h2>Login</h2>

      <!-- Login Form -->
      <form @submit.prevent="handleLogin">

        <!-- Email -->
        <div class="form-group">

          <label>Email</label>

          <input
            v-model="email"
            type="email"
            placeholder="Enter Email"
            required
          />

        </div>

        <!-- Password -->
        <div class="form-group">

          <label>Password</label>

          <input
            v-model="password"
            type="password"
            placeholder="Enter Password"
            required
          />

        </div>

        <!-- Login Button -->
        <button type="submit">
          Login
        </button>

      </form>

      <!-- Error Message -->
      <p
        v-if="error"
        class="error"
      >
        {{ error }}
      </p>

    </div>

  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useUserStore } from '../store/user'

// Reactive Variables
const email = ref('')
const password = ref('')
const error = ref('')

// Router
const router = useRouter()

// Pinia Store
const userStore = useUserStore()

// Login Function
function handleLogin() {

  // Empty Validation
  if (!email.value || !password.value) {

    error.value =
      'Please enter email and password'

    return
  }

  // Clear Error
  error.value = ''

  // Login User
  userStore.login()

  // Save User Details
  userStore.setUser(
    'Demo User',
    email.value
  )

  // Redirect Dashboard
  router.push('/dashboard')
}
</script>

<style scoped>

.login-container {
  min-height: 100vh;
  display: flex;
  justify-content: center;
  align-items: center;
  background: #f4f6f9;
}

.login-card {
  width: 350px;
  background: white;
  padding: 30px;
  border-radius: 12px;
  box-shadow: 0 0 12px rgba(0,0,0,0.1);
}

h1 {
  text-align: center;
  color: #2c3e50;
  margin-bottom: 10px;
}

h2 {
  text-align: center;
  margin-bottom: 25px;
  color: #444;
}

.form-group {
  margin-bottom: 18px;
}

label {
  display: block;
  margin-bottom: 6px;
  font-weight: bold;
}

input {
  width: 100%;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 6px;
  outline: none;
}

input:focus {
  border-color: #2c3e50;
}

button {
  width: 100%;
  padding: 12px;
  background: #2c3e50;
  color: white;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  font-size: 16px;
}

button:hover {
  background: #34495e;
}

.error {
  color: red;
  text-align: center;
  margin-top: 15px;
}

</style>