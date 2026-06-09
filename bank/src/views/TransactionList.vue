<template>
  <div class="container">

    <!-- CARD COMPONENT -->
    <Card title="Transaction List">
    

      <!-- ADD FORM -->
      <form @submit.prevent="addTransaction" class="form">

        <input
          v-model="newTransaction.name"
          type="text"
          placeholder="Transaction Name"
          required
        />

        <input
          v-model.number="newTransaction.amount"
          type="number"
          placeholder="Amount"
          required
        />

        <button type="submit">
          Add Transaction
        </button>

      </form>

      <!-- TABLE -->
      <table>

        <thead>
          <tr>
            <th>Transaction</th>
            <th>Amount</th>
            <th>Actions</th>
          </tr>
        </thead>

        <tbody>

          <tr
            v-for="transaction in transactions"
            :key="transaction.id"
          >

            <!-- EDIT MODE -->
            <template v-if="editId === transaction.id">

              <td>
                <input
                  v-model="editTransaction.name"
                  type="text"
                />
              </td>

              <td>
                <input
                  v-model.number="editTransaction.amount"
                  type="number"
                />
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

              <td>{{ transaction.name }}</td>

              <td>
                ₹{{ transaction.amount }}
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

    </Card>

  </div>
</template>

<script>
import Card from '../components/Card.vue'

export default {

  name: "TransactionCrud",

  components: {
    Card
  },

  data() {

    return {

      transactions: [
        { id: 1, name: "Salary", amount: 5000 },
        { id: 2, name: "Food", amount: 1200 },
        { id: 3, name: "Shopping", amount: 2500 }
      ],

      newTransaction: {
        name: "",
        amount: ""
      },

      editId: null,

      editTransaction: {
        id: null,
        name: "",
        amount: ""
      },

      nextId: 4
    }
  },

  methods: {

    // ADD
    addTransaction() {

      this.transactions.push({

        id: this.nextId++,

        name: this.newTransaction.name,

        amount: this.newTransaction.amount

      })

      this.newTransaction = {
        name: "",
        amount: ""
      }
    },

    // DELETE
    deleteTransaction(id) {

      this.transactions =
        this.transactions.filter(
          transaction => transaction.id !== id
        )
    },

    // EDIT
    startEdit(transaction) {

      this.editId = transaction.id

      this.editTransaction = {
        ...transaction
      }
    },

    // UPDATE
    updateTransaction() {

      const index =
        this.transactions.findIndex(
          transaction =>
            transaction.id === this.editTransaction.id
        )

      if (index !== -1) {

        this.transactions.splice(index, 1, {
          ...this.editTransaction
        })

      }

      this.cancelEdit()
    },

    // CANCEL
    cancelEdit() {

      this.editId = null

      this.editTransaction = {
        id: null,
        name: "",
        amount: ""
      }
    }

  }

}
</script>

<style scoped>

.container {
  display: flex;
  justify-content: center;
  padding: 40px;
  background: #f4f6f9;
  min-height: 100vh;
}

.form {
  display: flex;
  gap: 10px;
  margin-bottom: 20px;
}

input {
  flex: 1;
  padding: 10px;
  border-radius: 8px;
  border: 1px solid #ccc;
  outline: none;
}

input:focus {
  border-color: #4a90e2;
}

button {
  padding: 10px 14px;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  font-weight: bold;
}

table {
  width: 100%;
  border-collapse: collapse;
}

thead {
  background: #4a90e2;
  color: white;
}

th,
td {
  padding: 14px;
  border-bottom: 1px solid #ddd;
  text-align: center;
}

tr:hover {
  background: #f9f9f9;
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