<template>

  <div class="home-container">

    <div class="home-card">

      <!-- Logo -->
      <h1>
        🏦 My Bank
      </h1>

      <!-- Welcome Text -->
      <h2>
        Welcome to Banking App
      </h2>

      <p>
        Secure Online Banking System
      </p>

      <!-- Login Button -->
      <router-link to="/login">

        <button>
          Go To Login
        </button>

      </router-link>

    </div>

  </div>

</template>

<script setup>

// No script needed

</script>

<style scoped>

.home-container {

  min-height: 100vh;

  display: flex;

  justify-content: center;

  align-items: center;

  background: #f4f6f9;

}

.home-card {

  background: white;

  padding: 40px;

  border-radius: 12px;

  text-align: center;

  box-shadow: 0 4px 12px rgba(0,0,0,0.1);

  width: 350px;

}

h1 {

  color: #2c3e50;

  margin-bottom: 10px;

}

h2 {

  color: #34495e;

  margin-bottom: 10px;

}

p {

  color: #777;

  margin-bottom: 20px;

}

button {

  padding: 12px 20px;

  background: #2c3e50;

  color: white;

  border: none;

  border-radius: 8px;

  cursor: pointer;

  font-size: 16px;

  transition: 0.3s;

}

button:hover {

  background: #34495e;

}

</style>