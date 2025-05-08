package com.example.laundrygest_android.ui.dashboard

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel

class DashboardViewModel : ViewModel() {

    private val _clientCode = MutableLiveData<Int>().apply {
        value = -1
    }
    val clientCode: LiveData<Int> = _clientCode

    fun setClientCode(code : Int){
        _clientCode.value = code
    }
}