package com.example.laundrygest_android

import android.os.Build
import android.os.Bundle
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.laundrygest_android.data.CollectionDto
import com.example.laundrygest_android.databinding.ActivityCollectionDetailBinding
import com.example.laundrygest_android.tools.DateParser

class CollectionDetailActivity : AppCompatActivity() {
    lateinit var binding: ActivityCollectionDetailBinding
    lateinit var collection: CollectionDto
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        binding = ActivityCollectionDetailBinding.inflate(layoutInflater)
        setContentView(binding.root)
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main)) { v, insets ->
            val systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars())
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom)
            insets
        }

        collection = intent.getSerializableExtra("collection") as CollectionDto
        if (collection != null) {
            binding.collectionNumber.append(collection.number.toString())
            binding.collectionDate.append(DateParser.formatDate(collection.createdAt.toString()))
            binding.collectionClientName.text =
                collection.client.firstName + " " + collection.client.lastName
            binding.collectionClientTelephone.text = collection.client.telephone
            binding.collectionDueDate.append(DateParser.formatDate(collection.dueDate.toString()))
            binding.collectionDueTotal.append(String.format("%.2f€", collection.dueTotal))
            binding.colllectionTotal.append(String.format("%.2f€", collection.total))
            binding.collectionItemsRcv.adapter = collectionItemAdapter(collection.collectionItems)
            binding.collectionItemsRcv.layoutManager = LinearLayoutManager(this)
        }
    }
}