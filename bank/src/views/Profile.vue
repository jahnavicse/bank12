<template>
  <div class="profile">
    <h2>User Profile</h2>

    <!-- Display Profile -->
    <div v-if="!isEditing">
      <p><strong>Name:</strong> {{ userStore.name }}</p>
      <p><strong>Email:</strong> {{ userStore.email }}</p>
      <button @click="startEditing">Edit Profile</button>
    </div>

    <!-- Edit Profile Form -->
    <div v-else>
      <form @submit.prevent="saveProfile">
        <label>
          Name:
          <input v-model="form.name" type="text" required />
        </label>
        <label>
          Email:
          <input v-model="form.email" type="email" required />
        </label>
        <button type="submit">Save</button>
        <button type="button" @click="cancelEditing">Cancel</button>
      </form>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useUserStore } from '@/store/user'

const userStore = useUserStore()
const isEditing = ref(false)
const form = ref({ name: userStore.name, email: userStore.email })

function startEditing() {
  form.value = { name: userStore.name, email: userStore.email }
  isEditing.value = true
}

function saveProfile() {
  userStore.setUser(form.value.name, form.value.email)
  isEditing.value = false
}

function cancelEditing() {
  isEditing.value = false
}
</script>

<style scoped>
.profile {
  max-width: 400px;
  margin: auto;
  padding: 20px;
}
label {
  display: block;
  margin-bottom: 10px;
}
button {
  margin-right: 10px;
}
</style>
