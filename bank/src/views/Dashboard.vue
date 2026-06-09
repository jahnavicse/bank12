<template>

  <div class="dashboard">

    <!-- TITLE -->
    <h1 class="dashboard-title">
      Dashboard
    </h1>

    <!-- USER CARD -->
    <div class="card">

      <h2>
        Welcome,
        {{ userStore.email }}
      </h2>

      <h3 class="balance">
        Balance: ₹{{ userStore.balance }}
      </h3>

    </div>

    <!-- ADD TRANSACTION -->
    <div class="card">

      <h2>Add Transaction</h2>

      <form
        @submit.prevent="transfer"
        class="transfer-form"
      >

        <input
          v-model="amount"
          type="number"
          placeholder="Enter Amount"
          required
        />

        <select v-model="type">

          <option value="credit">
            Credit
          </option>

          <option value="debit">
            Debit
          </option>

        </select>

        <input
          v-model="category"
          type="text"
          placeholder="Category"
          required
        />

        <input
          v-model="description"
          type="text"
          placeholder="Description"
          required
        />

        <button type="submit">
          Add
        </button>

      </form>

      <!-- MESSAGE -->
      <p
        v-if="message"
        :class="status"
        class="message"
      >
        {{ message }}
      </p>

    </div>

    <!-- TRANSACTIONS -->
    <div class="card">

      <h2>Transactions</h2>

      <table>

        <thead>

          <tr>

            <th>ID</th>
            <th>Type</th>
            <th>Category</th>
            <th>Description</th>
            <th>Amount</th>
            <th>Date</th>
            <th>Actions</th>

          </tr>

        </thead>

        <tbody>

          <tr
            v-for="transaction in userStore.transactions"
            :key="transaction.id"
          >

            <!-- EDIT MODE -->
            <template
              v-if="editId === transaction.id"
            >

              <td>
                {{ transaction.id }}
              </td>

              <td>

                <select
                  v-model="editTransaction.type"
                >

                  <option value="credit">
                    Credit
                  </option>

                  <option value="debit">
                    Debit
                  </option>

                </select>

              </td>

              <td>

                <input
                  v-model="editTransaction.category"
                />

              </td>

              <td>

                <input
                  v-model="editTransaction.description"
                />

              </td>

              <td>

                <input
                  v-model.number="editTransaction.amount"
                  type="number"
                />

              </td>

              <td>
                {{ transaction.date }}
              </td>

              <td>

                <button
                  class="save-btn"
                  @click="updateTransaction"
                >
                  Save
                </button>

                <button
                  class="cancel-btn"
                  @click="cancelEdit"
                >
                  Cancel
                </button>

              </td>

            </template>

            <!-- NORMAL MODE -->
            <template v-else>

              <td>
                {{ transaction.id }}
              </td>

              <td>
                {{ transaction.type }}
              </td>

              <td>
                {{ transaction.category }}
              </td>

              <td>
                {{ transaction.description }}
              </td>

              <td>
                ₹{{ transaction.amount }}
              </td>

              <td>
                {{ transaction.date }}
              </td>

              <td>

                <button
                  class="edit-btn"
                  @click="startEdit(transaction)"
                >
                  Edit
                </button>

                <button
                  class="delete-btn"
                  @click="deleteTransaction(transaction.id)"
                >
                  Delete
                </button>

              </td>

            </template>

          </tr>

        </tbody>

      </table>

    </div>

  </div>

</template>

<script setup>
import { ref } from 'vue'
import { useUserStore } from '../store/user'

const userStore = useUserStore()

// FORM
const amount = ref('')
const type = ref('debit')
const category = ref('')
const description = ref('')

// MESSAGE
const message = ref('')
const status = ref('')

// EDIT
const editId = ref(null)

const editTransaction = ref({

  id: null,
  amount: '',
  type: '',
  category: '',
  description: '',
  date: ''

})

// ADD
function transfer() {

  if (
    !amount.value ||
    !category.value ||
    !description.value
  ) {

    message.value =
      'Please fill all fields'

    status.value = 'error'

    return
  }

  if (
    type.value === 'debit' &&
    Number(amount.value) >
    userStore.balance
  ) {

    message.value =
      'Insufficient balance'

    status.value = 'error'

    return
  }

  userStore.transactions.push({

    id: Date.now(),

    amount:
      Number(amount.value),

    type:
      type.value,

    category:
      category.value,

    description:
      description.value,

    date:
      new Date()
        .toLocaleString()

  })

  message.value =
    'Transaction added successfully'

  status.value = 'success'

  amount.value = ''
  type.value = 'debit'
  category.value = ''
  description.value = ''
}

// DELETE
function deleteTransaction(id) {

  userStore.transactions =
    userStore.transactions.filter(

      transaction =>
        transaction.id !== id

    )
}

// START EDIT
function startEdit(transaction) {

  editId.value = transaction.id

  editTransaction.value = {

    ...transaction

  }
}

// UPDATE
function updateTransaction() {

  const index =
    userStore.transactions.findIndex(

      transaction =>
        transaction.id ===
        editTransaction.value.id

    )

  if (index !== -1) {

    userStore.transactions.splice(

      index,

      1,

      {

        ...editTransaction.value,

        amount:
          Number(
            editTransaction.value.amount
          )

      }

    )
  }

  cancelEdit()

  message.value =
    'Transaction updated successfully'

  status.value = 'success'
}

// CANCEL
function cancelEdit() {

  editId.value = null

  editTransaction.value = {

    id: null,
    amount: '',
    type: '',
    category: '',
    description: '',
    date: ''

  }
}
</script>

<style scoped>

.dashboard {

  padding: 30px;

  background: #f4f6f9;

  min-height: 100vh;

}

.dashboard-title {

  text-align: center;

  margin-bottom: 20px;

  color: #2c3e50;

}

.card {

  background: white;

  padding: 25px;

  border-radius: 12px;

  margin-bottom: 25px;

  box-shadow: 0 4px 12px rgba(0,0,0,0.08);

}

.balance {

  color: green;

}

.transfer-form {

  display: flex;

  gap: 12px;

  flex-wrap: wrap;

}

input,
select {

  padding: 10px;

  border: 1px solid #ccc;

  border-radius: 8px;

  flex: 1;

}

button {

  padding: 10px 15px;

  border: none;

  border-radius: 8px;

  cursor: pointer;

  font-weight: bold;

}

.transfer-form button {

  background: #2c3e50;

  color: white;

}

.message {

  margin-top: 15px;

}

.success {

  color: green;

}

.error {

  color: red;

}

table {

  width: 100%;

  border-collapse: collapse;

  margin-top: 20px;

}

thead {

  background: #2c3e50;

  color: white;

}

th,
td {

  padding: 12px;

  border-bottom: 1px solid #ddd;

  text-align: center;

}

.edit-btn {

  background: orange;

  color: white;

  margin-right: 5px;

}

.delete-btn {

  background: crimson;

  color: white;

}

.save-btn {

  background: green;

  color: white;

  margin-right: 5px;

}

.cancel-btn {

  background: gray;

  color: white;

}

</style>