// src/store/user.js

import { defineStore } from 'pinia'

export const useUserStore = defineStore('user', {

  state: () => ({

    // LOGIN
    isLoggedIn: false,

    // USER
    name: '',
    email: '',

    // TRANSACTIONS
    transactions: [

      {
        id: 1,
        date: '2026-06-01',
        amount: 1000,
        type: 'credit',
        category: 'Salary',
        description: 'Monthly Salary'
      },

      {
        id: 2,
        date: '2026-06-02',
        amount: 500,
        type: 'debit',
        category: 'Food',
        description: 'Restaurant Payment'
      }

    ]

  }),

  getters: {

    // BALANCE
    balance: (state) => {

      return state.transactions.reduce(

        (total, transaction) => {

          if (
            transaction.type === 'credit'
          ) {

            return (
              total +
              Number(transaction.amount)
            )
          }

          if (
            transaction.type === 'debit'
          ) {

            return (
              total -
              Number(transaction.amount)
            )
          }

          return total

        },

        0
      )
    }

  },

  actions: {

    // LOGIN
    login() {

      this.isLoggedIn = true

    },

    // LOGOUT
    logout() {

      this.isLoggedIn = false

      this.name = ''
      this.email = ''

    },

    // SET USER
    setUser(name, email) {

      this.name = name

      this.email = email

    },

    // CREATE TRANSACTION
    addTransaction(transaction) {

      this.transactions.push({

        id: Date.now(),

        date:
          transaction.date,

        amount:
          Number(transaction.amount),

        type:
          transaction.type,

        category:
          transaction.category,

        description:
          transaction.description

      })

    },

    // DELETE TRANSACTION
    deleteTransaction(id) {

      this.transactions =
        this.transactions.filter(

          transaction =>
            transaction.id !== id

        )

    },

    // UPDATE TRANSACTION
    updateTransaction(updatedTransaction) {

      const index =
        this.transactions.findIndex(

          transaction =>
            transaction.id ===
            updatedTransaction.id

        )

      if (index !== -1) {

        this.transactions[index] = {

          ...updatedTransaction,

          amount:
            Number(
              updatedTransaction.amount
            )

        }

      }

    }

  }

})