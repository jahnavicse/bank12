```vue id="9t0m2f"
<template>
  <div class="container">
    <div class="card">
      <h2>Transaction Manager</h2>

      <!-- FORM -->
      <div class="form">
        <input v-model="newTransaction.date" type="date" />

        <select v-model="newTransaction.type">
          <option disabled value="">Select Type</option>
          <option>Credit</option>
          <option>Debit</option>
        </select>

        <input
          v-model="newTransaction.amount"
          type="number"
          placeholder="Amount"
        />

        <input
          v-model="newTransaction.category"
          type="text"
          placeholder="Category"
        />

        <input
          v-model="newTransaction.description"
          type="text"
          placeholder="Description"
        />

        <button @click="addTransaction">
          Add Transaction
        </button>
      </div>

      <!-- TABLE -->
      <table>
        <thead>
          <tr>
            <th>Date</th>
            <th>Type</th>
            <th>Amount</th>
            <th>Category</th>
            <th>Description</th>
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
                <input v-model="editTransaction.date" type="date" />
              </td>

              <td>
                <select v-model="editTransaction.type">
                  <option>Credit</option>
                  <option>Debit</option>
                </select>
              </td>

              <td>
                <input
                  v-model="editTransaction.amount"
                  type="number"
                />
              </td>

              <td>
                <input
                  v-model="editTransaction.category"
                  type="text"
                />
              </td>

              <td>
                <input
                  v-model="editTransaction.description"
                  type="text"
                />
              </td>

              <td>
                <button class="save-btn"
                  @click="updateTransaction(transaction.id)">
                  Save
                </button>

                <button class="cancel-btn"
                  @click="cancelEdit">
                  Cancel
                </button>
              </td>
            </template>

            <!-- NORMAL MODE -->
            <template v-else>
              <td>{{ transaction.date }}</td>

              <td>
                <span
                  :class="transaction.type === 'Credit'
                    ? 'credit'
                    : 'debit'"
                >
                  {{ transaction.type }}
                </span>
              </td>

              <td>₹{{ transaction.amount }}</td>

              <td>{{ transaction.category }}</td>

              <td>{{ transaction.description }}</td>

              <td>
                <button class="edit-btn"
                  @click="startEdit(transaction)">
                  Edit
                </button>

                <button class="delete-btn"
                  @click="removeTransaction(transaction.id)">
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

<script>
export default {
  name: "Card",

  data() {
    return {
      editId: null,

      newTransaction: {
        date: "",
        type: "",
        amount: "",
        category: "",
        description: ""
      },

      editTransaction: {
        date: "",
        type: "",
        amount: "",
        category: "",
        description: ""
      },

      transactions: [
        {
          id: 1,
          date: "2026-06-01",
          type: "Credit",
          amount: 5000,
          category: "Salary",
          description: "Monthly Salary"
        },
        {
          id: 2,
          date: "2026-06-02",
          type: "Debit",
          amount: 1200,
          category: "Food",
          description: "Restaurant Bill"
        },
        {
          id: 3,
          date: "2026-06-03",
          type: "Debit",
          amount: 800,
          category: "Travel",
          description: "Cab Charges"
        }
      ]
    }
  },

  methods: {
    // ADD
    addTransaction() {
      if (
        !this.newTransaction.date ||
        !this.newTransaction.type ||
        !this.newTransaction.amount
      ) {
        return
      }

      this.transactions.push({
        id: Date.now(),
        ...this.newTransaction
      })

      this.newTransaction = {
        date: "",
        type: "",
        amount: "",
        category: "",
        description: ""
      }
    },

    // DELETE
    removeTransaction(id) {
      this.transactions = this.transactions.filter(
        transaction => transaction.id !== id
      )
    },

    // START EDIT
    startEdit(transaction) {
      this.editId = transaction.id

      this.editTransaction = {
        ...transaction
      }
    },

    // UPDATE
    updateTransaction(id) {
      const index = this.transactions.findIndex(
        transaction => transaction.id === id
      )

      if (index !== -1) {
        this.transactions[index] = {
          id,
          ...this.editTransaction
        }
      }

      this.editId = null
    },

    // CANCEL
    cancelEdit() {
      this.editId = null
    }
  }
}
</script>

<style scoped>
.container {
  display: flex;
  justify-content: center;
  padding: 30px;
  background: #f5f7fb;
  min-height: 100vh;
}

.card {
  width: 100%;
  max-width: 1200px;
  background: white;
  padding: 25px;
  border-radius: 14px;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
}

h2 {
  text-align: center;
  margin-bottom: 25px;
  color: #333;
}

.form {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(160px, 1fr));
  gap: 12px;
  margin-bottom: 25px;
}

input,
select {
  padding: 10px;
  border-radius: 8px;
  border: 1px solid #ccc;
  outline: none;
}

input:focus,
select:focus {
  border-color: #4a90e2;
}

button {
  border: none;
  padding: 10px 14px;
  border-radius: 8px;
  cursor: pointer;
  font-weight: 600;
}

button:hover {
  opacity: 0.9;
}

table {
  width: 100%;
  border-collapse: collapse;
  overflow: hidden;
  border-radius: 10px;
}

thead {
  background: #4a90e2;
  color: white;
}

th,
td {
  padding: 14px;
  text-align: center;
  border-bottom: 1px solid #eee;
}

tbody tr:hover {
  background: #f9f9f9;
}

.credit {
  color: green;
  font-weight: bold;
}

.debit {
  color: red;
  font-weight: bold;
}

.edit-btn {
  background: #f0ad4e;
  color: white;
  margin-right: 5px;
}

.delete-btn {
  background: #d9534f;
  color: white;
}

.save-btn {
  background: #5cb85c;
  color: white;
  margin-right: 5px;
}

.cancel-btn {
  background: #777;
  color: white;
}
</style>
