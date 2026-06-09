<script setup>
import Navbar from './components/Navbar.vue'
</script>

<template>
  <div class="app-container">

    <!-- Navbar -->
    <Navbar />

    <!-- Page Content -->
    <div class="page-content">
      <router-view />
    </div>

  </div>
</template>

<style scoped>
/* Full App Container */
.app-container {
  min-height: 100vh;
  background-color: #f5f7fa;
  font-family: Arial, Helvetica, sans-serif;
}

/* Router Page Content */
.page-content {
  padding: 20px;
}
</style>