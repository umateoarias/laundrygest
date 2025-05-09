package com.example.laundrygest_android

import android.content.Intent
import android.os.Bundle
import android.widget.Toast
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import androidx.core.view.isNotEmpty
import com.example.laundrygest_android.data.LaundrygestCrudApi
import com.example.laundrygest_android.databinding.ActivityMainBinding

class MainActivity : AppCompatActivity() {
    lateinit var binding: ActivityMainBinding
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main)) { v, insets ->
            val systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars())
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom)
            insets
        }
        val crudApi = LaundrygestCrudApi()
        binding.btLogin.setOnClickListener {
            val username = binding.userField.text!!.toString()
            val password = binding.passwordField.text!!.toString()
            if (username.isNotEmpty() && password.isNotEmpty()) {
                val client = crudApi.getClientLogin(username, password)
                if (client != null) {
                    val activityIntent = Intent(this, PrincipalActivity::class.java)
                    activityIntent.putExtra("client_code", client.code)
                    startActivity(activityIntent)
                } else {
                    Toast.makeText(this, "Login incorrecte, torna a intentar-ho", Toast.LENGTH_LONG)
                        .show()
                }
            }
        }
    }
}